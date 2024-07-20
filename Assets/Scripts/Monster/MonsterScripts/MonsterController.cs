using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterInfo))]
public abstract class MonsterController : MonoBehaviour, IHitable
{

    public MonsterInfo monsterInfo;

    Coroutine _rotationCoroutine;

    public Animator animator;

    public MONSTER_TYPE monster_type;

    public List<System.Enum> patturnIndexes;

    // _characterGotIntoArea가 true가 되고, _isMove가 true 가 될 때
    // 움직일 수 있게 사용
    public bool _isMove = false;
    public bool _isDead = false;

    public bool _doingSomeAction = false;

    // 맨 처음에 본인 참조하게
    // 얘는 굳이 상속받은 애가 건들 필요없을 것 같음
    public Vector3 _monsterOriginPosition;
    public Quaternion _monsterOriginRotation;

    
    // trigger가 설정할 것 ()
    // 캐릭터 위치가 null 일때 본래 자리로 돌아가게 짜야할 듯
    // 얘네도 굳이 상속받은 애가 건들 필욘없을 것 같음
    public bool _characterGotIntoArea = false;
    public GameObject PlayerObject;
    public Transform _characterTransfrom;

    


    // State들
    // attack은 보스는 공격 1개 스킬 1개(2페이즈는 스킬이 하나 생김) 일반은 1개 중에 뽑기
    EnemyState currentState;

    public EnemyIdleState idleState;
    public EnemyMoveState moveState;
    public EnemyAttackState attackState;
    public EnemyDeadState deadState;



    // 상속받은 스크립트에서 재정의할 것 다 하고 base.Awake() 해야 함
    // 본인의 위치와 각자의 MaxHp만큼 현재 체력을 설정하게 해놓음
    // ㄴ start에서 
    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }


    protected void Start()
    {
        monsterInfo = GetComponent<MonsterInfo>();

        monsterInfo._currentHP = monsterInfo._maxHP;

        _monsterOriginPosition = transform.position;

        _monsterOriginRotation = transform.rotation;

        PlayerObject = PlayerGameobjectManager.instance.playerObject;

        idleState = new EnemyIdleState(this);

        moveState = new EnemyMoveState(this);

        switch (monster_type)
        {
            case MONSTER_TYPE.BOSS_MONSTER:
                attackState = new BossAttackState(this);
                break;
            case MONSTER_TYPE.MELEE_MONSTER:
                attackState = new MeleeAttackState(this);
                break;
            case MONSTER_TYPE.RANGED_MONSTER:
                attackState = new RangedAttackState(this);
                break;
        }

        deadState = new EnemyDeadState(this);

        currentState = idleState;
        currentState.Enter();

    }

    // 플레이어나 몬스터가 참조할 수 있도록 public으로 
    public virtual void Hit(float damage)
    {
        monsterInfo._currentHP -= damage;

        if (monsterInfo._currentHP <= 0)
        {
            

            if (!_isDead)
            {

                TransitionToState(deadState);
            }

            return;

        }

        // 맞았을 때 움직임으로 바꾸게
        // 처음부터 몬스터는 캐릭터의 오브젝트를 참조는 하고 있다가
        // 맞았을 때 움직일 때 트랜스폼을 참조한다.
        if (currentState == idleState)
        {
            //print(currentState);
            TransitionToState(moveState);

            monsterInfo._IsAttacked = true;
        }

    }

    // 상대방의 체력에 영향을 줘야 하는데 함수 자체가 아직 애매해서 미정
    void Attack()
    {

    }

    private void Update()
    {
        currentState.Update();
        
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
        if (PlayerObject == null)
        {
            PlayerObject = targetObject;
        }
        
    }

    // 캐릭터 위치를 불러오는 함수
    public void LoadCharacterTransform()
    {
        _characterTransfrom = PlayerObject.transform;

        _isMove = true;

        animator.SetBool("IsMove", true);

    }
    public void SetCharacterTransformNull()
    {
        _characterTransfrom = null;
        monsterInfo._IsAttacked = false;
    }
    // 회전하는 코루틴
    // 제자리에서 걸어서 회전하게 만들어야 함
    // 혹은 내가 IK를 조절하고 애니메이션 재설정
    // 혹은 그냥 어색하게 회전하기
    // 걷는 애니메이션 활성화 및 
    // 회전을 안해도 될 것 같기도
    //IEnumerator RotateWhenReturn()
    //{
    //    while (true)
    //    {
    //        if (Quaternion.Angle(transform.rotation, _monsterOriginRotation) < 10.0f)
    //        {
    //            yield break;
    //        }

    //        transform.rotation = Quaternion.Slerp(transform.rotation, _monsterOriginRotation, Time.deltaTime * 3.0f);
    //        yield return null; // 매 프레임마다 대기
    //    }
    //}
}
public enum MONSTER_TYPE
{
    BOSS_MONSTER,
    MELEE_MONSTER,
    RANGED_MONSTER
}