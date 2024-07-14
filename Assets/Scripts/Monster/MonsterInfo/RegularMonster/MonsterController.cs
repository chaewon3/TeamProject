using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterInfo))]
public abstract class MonsterController : MonoBehaviour, IHitable
{
    MonsterInfo monsterInfo;

    Coroutine _rotationCoroutine;

    // _characterGotIntoArea가 true가 되고, _isMove가 true 가 될 때
    // 움직일 수 있게 사용
    protected bool _isMove = false;

    // 맨 처음에 본인 참조하게
    protected Transform _monsterOriginTransform;

    // trigger가 설정할 것 ()
    // 캐릭터 위치가 null 일때 본래 자리로 돌아가게 짜야할 듯
    protected bool _characterGotIntoArea = false;
    protected Transform _characterTransfrom;

    // 상속받은 스크립트에서 재정의할 것 다 하고 base.Awake() 해야 함
    // 본인의 위치와 각자의 MaxHp만큼 현재 체력을 설정하게 해놓음
    protected virtual void Awake()
    {
        monsterInfo = GetComponent<MonsterInfo>();

        monsterInfo._currentHP = monsterInfo._maxHP;
        _monsterOriginTransform = this.transform;
    }

    // 플레이어나 몬스터가 참조할 수 있도록 public으로 
    public void Hit(float damage)
    {
        monsterInfo._currentHP -= damage;
    }

    // 상대방의 체력에 영향을 줘야 하는데 함수 자체가 아직 애매해서 미정
    void Attack()
    {

    }


    // Start 코루틴에서 move의 여부와 스킬이나 공격을 쓸 건지만  정하고 update는 캐릭터의 위치가 null이 아니고 move가 true일 때 움직이게
    /*
    private IEnumerator Start()
    {

        while (OnGame)
        {
            if(GameManager.Instance.Playing)
            {
                List<int> zeroIndexes = new List<int>();

                for (int i = 0; i < foxIndexArr.Length; ++i)
                {
                    if (foxIndexArr[i] == 0)
                    {
                        zeroIndexes.Add(i);
                    }
                }

                if (zeroIndexes.Count > 0)
                {
                    int randomIndex = zeroIndexes[Random.Range(0, zeroIndexes.Count)];
                    foxIndexArr[randomIndex] = 1;
                    //print($"randomIndex : {randomIndex}");
                    FoxSpawn(randomIndex);
                }

            }
            yield return new WaitForSeconds(foxSpawnPeriod);

        }
    }
    */

    // 스타트가 아닌 trigger enter되었을 때 활성화하게 만드는 게 좋을 것 같음
    // 스타트에 해버리면 멀리있는 객체도 사용하지 않음에도 돌아가서 성능에 영향을 줄 것 같음
    // 멀리 있으면 비활성화해 사용하면 되지만 안함
    private IEnumerator Start()
    {
        while (true)
        {
            // 체크를 해야 하나? 
            // 트리거에서 들어갔을 때 시작하고 나갈 때 stop할건데? 

            // 우선 캐릭터가 범위 안에 들어와 있나 체크
            if (_characterGotIntoArea)
            {
                // 캐릭터 위치 받아오기
                LoadCharacterTransform();

                // isMove가 활성화되어 있나 체크
                if (_isMove)
                {
                    // 움직이고 있다면 1초 후 위치 찾으러 가기
                    yield return new WaitForSeconds(1.0f);
                }
                else
                {
                    // 여기서 스킬 체크
                    yield return new WaitForSeconds(5.0f);
                }


            }
        }


    }


    private void Update()
    {
        if (_isMove && _characterGotIntoArea)
        {
            // 캐릭터 트랜스폼을 찾아 움직이는 함수
            // 자기 자리로 돌아갈 때는 _isMove는 true이고 null일때로 하려 했지만
            // 그냥 얘는 찾았을 때 움직임만 관여하고 start에서 1초마다 찾을 때 캐릭터의 위치가 null이면
            // 원래 자리로 가는 함수를 실행하는 것이 더 좋아보임
            // 그럼 null을 여기서 매번 체크는 안해도 될 것 같음
            if (_rotationCoroutine != null)
            {
                StopCoroutine(_rotationCoroutine);
                _rotationCoroutine = null;

            }

            MonsterMove(_characterTransfrom, monsterInfo._attackDetectRange);
        }

        if (_isMove && !_characterGotIntoArea && (Vector3.SqrMagnitude(transform.position - _monsterOriginTransform.position) > 1))
        {
            MonsterMove(_monsterOriginTransform, monsterInfo._returnStopRange);

            if (!_isMove)
            {

                if (_rotationCoroutine == null)
                {
                    _rotationCoroutine = StartCoroutine(RotateWhenReturn());
                }
            }

        }
    }

    void MonsterMove(Transform target, float stopRange)
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = target.position;

        // 델타 타임 앞에 1은 speed로 
        transform.position = Vector3.MoveTowards(currentPosition, new Vector3(targetPosition.x, currentPosition.y, targetPosition.z), 1 * Time.deltaTime);

        if (Vector3.SqrMagnitude(currentPosition - targetPosition) <= (stopRange * stopRange))
        {
            _isMove = false;
        }
    }

    // trigger Enter에서 호출할 함수
    // 닭 무리 할 때처럼 한 방에서 모두 활성화 시키게 만들기
    public void CharacterGotIntoArea()
    {
        _characterGotIntoArea = true;
        // 이게 아니라 지금 start인 코루틴을 호출
        StartCoroutine(SearchingCharacter());
    }

    // trigger Exit에서 호출할 함수
    // 한 방에서 모두 비활성화 시키기 
    public void CharacterGotOutArea()
    {
        _characterGotIntoArea = false;

        // 위에 있는 transform을 null로 여기서 하지말고 

    }


    // 캐릭터 위치를 불러오는 함수
    void LoadCharacterTransform()
    {

    }

    // 캐릭터가 트리거 밖으로 나갔을 때 호출할 함수
    // 자신의 위치까지 이동한다.
    // bool 변수하나 더 만들고 update에서 처리하기
    // 그냥 intoArea를 false면 실행되게 하는게 맞는거 아닌가
    // 굳이 이건 필요 없고 false일 때 target이 본인의 origin으로 
    // 필요없진 않고 exit할때 한번 호출해서 사용하면 될 듯
    void SetCharacterTransformNull()
    {
        // 이게 null일 때 몬스터들이 자신의 원래 위치로 이동하게 만들기
        // 근데 굳이 null로 바꾸어야 하나 
        // 바꾸어야 한다 메모리가 잡혀있기 떄문에 가비지 컬렉션을 통해 메모리를 회수해 와야 함

        _characterTransfrom = null;
    }

    // 상속 받아서 나온 숫자에 따라 함수 설정하게 만들기
    // 얘는 숫자만 뽑는거라 굳이 virtual이어야 할 이유는 없어 보임
    protected virtual void SelectPattern()
    {
        List<int> zeroIndexes = new List<int>();

        for (int i = 0; i < monsterInfo._monsterBehaviourPool.Length; ++i)
        {
            if (monsterInfo._monsterBehaviourPool[i] == 0)
            {
                zeroIndexes.Add(i);
            }
        }

        if (zeroIndexes.Count > 0)
        {
            int randomIndex = zeroIndexes[Random.Range(0, zeroIndexes.Count)];
            monsterInfo._monsterBehaviourPool[randomIndex] = 1;
        }
    }


    // 쓸모없는 코드 얜 사용하지 않음
    // _characterGotIntoArea가 true가 되었을 때 한번 호출
    // 게임 매니저에서 플레이어의 위치를 받아올 수 있도록 하고
    // 그걸 1초마다 불러오기
    IEnumerator SearchingCharacter()
    {
        while (_characterGotIntoArea)
        {
            yield return new WaitForSeconds(1.0f);

            // 대략 이렇게 불러와서 사용하기
            Transform loadedTransform = null;
            _characterTransfrom = loadedTransform;
        }

        // 다음 호출까지 대기
        yield return null;
    }

    // 회전하는 코루틴
    // 제자리에서 걸어서 회전하게 만들어야 함
    // 혹은 내가 IK를 조절하고 애니메이션 재설정
    // 혹은 그냥 어색하게 회전하기
    IEnumerator RotateWhenReturn()
    {
        while (true)
        {

            yield return null;
        }
    }
}
