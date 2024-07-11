using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterInfo : MonoBehaviour, IHitable
{
    // 인스펙터에서 설정할 수 있는 거
    protected float _maxHP;
    protected float _currentHP;
    protected float _attackDamage;


    // _characterGotIntoArea가 true가 되고, _isMove가 true 가 될 때
    // 움직일 수 있게 사용
    protected bool _isMove = false;

    // 맨 처음에 본인 참조하게
    protected Transform _monsterOriginTransform;

    // trigger가 설정할 것 ()
    // 캐릭터 위치가 null 일때 본래 자리로 돌아가게 짜야할 듯
    protected bool _characterGotIntoArea = false;
    protected Transform _characterTransfrom;


    // 상속받는 몬스터나 보스 몬스터 스크립트에서 재정의 (패턴의 개수만큼)
    protected int[] _monsterBehaviourPool;


    // 상속받은 스크립트에서 재정의할 것 다 하고 base.Awake() 해야 함
    // 본인의 위치와 각자의 MaxHp만큼 현재 체력을 설정하게 해놓음
    protected virtual void Awake()
    {
        _currentHP = _maxHP;
        _monsterOriginTransform = this.transform;
    }

    // 플레이어나 몬스터가 참조할 수 있도록 public으로 
    public void Hit(float damage)
    {
        _currentHP -= damage;
    }

    // 상대방의 체력에 영향을 줘야 하는데 함수 자체가 아직 애매해서 미정
    void Attack()
    {

    }


    // Start 코루틴에서 move의 여부와 스킬이나 공격을 쓸 건지만  정하고 update는 캐릭터의 위치가 null이 아니고 move가 true일 때 움직이가
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
        
    }


    // trigger Enter에서 호출할 함수
    // 닭 무리 할 때처럼 한 방에서 모두 활성화 시키게 만들기
    public void CharacterGotIntoArea()
    {
        _characterGotIntoArea = true;
        StartCoroutine(SearchingCharacter());
    }

    // trigger Exit에서 호출할 함수
    // 한 방에서 모두 비활성화 시키기 
    public void CharacterGotOutArea()
    {
        _characterGotIntoArea = false;

        // 이게 null일 때 몬스터들이 자신의 원래 위치로 이동하게 만들기
        _characterTransfrom = null;
        
    }
    

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

}
