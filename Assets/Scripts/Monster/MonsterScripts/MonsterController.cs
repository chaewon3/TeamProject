using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterInfo))]
public abstract class MonsterController : MonoBehaviour, IHitable
{

    protected MonsterInfo monsterInfo;

    Coroutine _rotationCoroutine;

    // _characterGotIntoArea가 true가 되고, _isMove가 true 가 될 때
    // 움직일 수 있게 사용
    public bool _isMove = false;

    public bool _doingSomeAction = false;

    // 맨 처음에 본인 참조하게
    //protected Transform _monsterOriginTransform;
    // 얘는 굳이 상속받은 애가 건들 필요없을 것 같음
    Vector3 _monsterOriginPosition;
    Quaternion _monsterOriginRotation;

    EnemyState currentState;


    // trigger가 설정할 것 ()
    // 캐릭터 위치가 null 일때 본래 자리로 돌아가게 짜야할 듯
    // 얘네도 굳이 상속받은 애가 건들 필욘없을 것 같음
    public bool _characterGotIntoArea = false;
    public GameObject PlayerObject;
    Transform _characterTransfrom;

    public Animator animator;

    public EnemyIdleState idleState;
    public EnemyMoveState moveState;
    public EnemyAttackState attackState;



    // 상속받은 스크립트에서 재정의할 것 다 하고 base.Awake() 해야 함
    // 본인의 위치와 각자의 MaxHp만큼 현재 체력을 설정하게 해놓음
    // ㄴ start에서 
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }


    protected virtual void Start()
    {
        monsterInfo = GetComponent<MonsterInfo>();

        monsterInfo._currentHP = monsterInfo._maxHP;


        _monsterOriginPosition = transform.position;

        _monsterOriginRotation = transform.rotation;

        //_monsterOriginTransform;
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

    private void Update()
    {
        if (_isMove && _characterGotIntoArea)
        {
            // 캐릭터 트랜스폼을 찾아 움직이는 함수
            // 자기 자리로 돌아갈 때는 _isMove는 true이고 null일때로 하려 했지만
            // 그냥 얘는 찾았을 때 움직임만 관여하고 start에서 1초마다 찾을 때 캐릭터의 위치가 null이면
            // 원래 자리로 가는 함수를 실행하는 것이 더 좋아보임
            // 그럼 null을 여기서 매번 체크는 안해도 될 것 같음
        }

        if (_isMove && !_characterGotIntoArea)
        {
        }

        if (_isMove)
        {
            if (_characterGotIntoArea)
            {
                if (_rotationCoroutine != null)
                {
                    StopCoroutine(_rotationCoroutine);
                    _rotationCoroutine = null;

                    print("rotateCoroutine Stop");
                }

                MonsterMove(_characterTransfrom.position, monsterInfo._attackDetectRange);
            }
            else
            {
                MonsterMove(_monsterOriginPosition, monsterInfo._returnStopRange);
                // && (Vector3.SqrMagnitude(transform.position - _monsterOriginPosition) > 1) 이 조건이 포함되어야 하나

                if (!_isMove)
                {

                    print("return and rotate");

                    if (_rotationCoroutine == null)
                    {
                        _rotationCoroutine = StartCoroutine(RotateWhenReturn());
                        print("rotateCoroutine Start");
                    }
                }
            }
        }




        //print($" ismove = {_isMove}");
    }

    void MonsterMove(Vector3 targetPos, float stopRange)
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = targetPos;

        Vector3 direction = (targetPosition - currentPosition).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3.0f * Time.deltaTime);
        }


        // 델타 타임 앞에 1은 speed로 
        transform.position = Vector3.MoveTowards(currentPosition, new Vector3(targetPosition.x, currentPosition.y, targetPosition.z), monsterInfo._moveSpeed * Time.deltaTime);

        if (Vector3.SqrMagnitude(currentPosition - targetPosition) <= (stopRange * stopRange))
        {
            // 여기서 isMove = false를 하지 않고 스킬이나 공격 쪽에서 해당 애니메이션동안 false로 바꾸기만 하면 될 듯

            _isMove = false;

            animator.SetBool("IsMove", false);

            int rndValue = SelectPattern();

            StartCoroutine(doSomething(rndValue));

        }
    }

    protected virtual IEnumerator doSomething(int index)
    {
        switch (index)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;

            default:
                break;
        }

        _doingSomeAction = true;
        yield return new WaitForSeconds(2.0f);
        _doingSomeAction = false;

    }

    public void TransitionToState(EnemyState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    // trigger Enter에서 호출할 함수
    // 닭 무리 할 때처럼 한 방에서 모두 활성화 시키게 만들기
    public void CharacterGotIntoArea()
    {
        _characterGotIntoArea = true;

        // 범위안에 들어왔다면 코루틴은 계속 돌게
        // 몬스터 활성화 느낌
        StartCoroutine(OnCharaterEnterArea());
    }

    // trigger Exit에서 호출할 함수
    // 한 방에서 모두 비활성화 시키기 
    public void CharacterGotOutArea()
    {
        _characterGotIntoArea = false;

    }


    // 범위 안에 들어왔다면 캐릭터 오브젝트를 참조하게 만들어
    // loadTransform에서 오브젝트의 현재 위치를 참조하게 만듦
    public void LoadCharacterObject(GameObject targetObject)
    {
        PlayerObject = targetObject;
    }

    // 캐릭터 위치를 불러오는 함수
    public void LoadCharacterTransform()
    {
        _characterTransfrom = PlayerObject.transform;

        _isMove = true;

        animator.SetBool("IsMove", true);
    }

    // 캐릭터가 트리거 밖으로 나갔을 때 호출할 함수
    // 자신의 위치까지 이동한다.
    // bool 변수하나 더 만들고 update에서 처리하기
    // 그냥 intoArea를 false면 실행되게 하는게 맞는거 아닌가
    // 굳이 이건 필요 없고 false일 때 target이 본인의 origin으로 
    // 필요없진 않고 exit할때 한번 호출해서 사용하면 될 듯
    public void SetCharacterTransformNull()
    {
        // 이게 null일 때 몬스터들이 자신의 원래 위치로 이동하게 만들기
        // 근데 굳이 null로 바꾸어야 하나 
        // 바꾸어야 한다 메모리가 잡혀있기 떄문에 가비지 컬렉션을 통해 메모리를 회수해 와야 함

        PlayerObject = null;
        _characterTransfrom = null;
        //_isMove = false;

    }

    // 상속 받아서 나온 숫자에 따라 함수 설정하게 만들기
    // 얘는 숫자만 뽑는거라 굳이 virtual이어야 할 이유는 없어 보임

    protected List<int> zeroIndexes;
    int SelectPattern()
    {
        zeroIndexes = new List<int>();
        int randomIndex;

        for (int i = 0; i < monsterInfo._monsterBehaviourPool.Length; ++i)
        {
            if (monsterInfo._monsterBehaviourPool[i] == 0)
            {
                zeroIndexes.Add(i);
            }
        }

        if (zeroIndexes.Count > 0)
        {
            randomIndex = zeroIndexes[Random.Range(0, zeroIndexes.Count)];
            monsterInfo._monsterBehaviourPool[randomIndex] = 1;
        }
        else
        {
            randomIndex = -1;
        }


        
        return randomIndex;
    }

    IEnumerator OnCharaterEnterArea()
    {
        while (true)
        {

            // 우선 캐릭터가 범위 안에 들어와 있나 체크
            if (_characterGotIntoArea)
            {
                // 캐릭터 위치 받아오기

                if (PlayerObject != null && !_doingSomeAction)
                {
                    LoadCharacterTransform();
                }

                // isMove가 활성화되어 있나 체크
                if (_isMove)
                {
                    // 움직이고 있다면 1초 후 위치 찾으러 가기
                    print("Searching Character");

                    yield return new WaitForSeconds(1.0f);
                }
                else
                {
                    print("skill or attack");
                    // 여기서 스킬 체크
                    yield return new WaitForSeconds(5.0f);
                }


            }
            else
            {
                // 이거일때 무한 루프에 빠짐
                //print($"_characterGotIntoArea : {_characterGotIntoArea}");
                yield return new WaitForSeconds(1.0f);
            }
            
        }
    }

    // 회전하는 코루틴
    // 제자리에서 걸어서 회전하게 만들어야 함
    // 혹은 내가 IK를 조절하고 애니메이션 재설정
    // 혹은 그냥 어색하게 회전하기
    // 걷는 애니메이션 활성화 및 
    IEnumerator RotateWhenReturn()
    {
        while (true)
        {
            if (Quaternion.Angle(transform.rotation, _monsterOriginRotation) < 10.0f)
            {
                yield break;
            }

            print("Monster Rotating");
            transform.rotation = Quaternion.Slerp(transform.rotation, _monsterOriginRotation, Time.deltaTime * 3.0f);
            yield return null; // 매 프레임마다 대기
        }
    }
}
