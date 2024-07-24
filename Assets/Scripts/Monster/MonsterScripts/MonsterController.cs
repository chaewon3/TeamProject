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
    public bool _isHit = false;

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
    public EnemyState currentState;

    public EnemyIdleState idleState;
    public EnemyMoveState moveState;
    public EnemyAttackState attackState;
    public EnemyDeadState deadState;
    public EnemyHitState hitState;

    // 상속받은 스크립트에서 재정의할 것 다 하고 base.Awake() 해야 함
    // 본인의 위치와 각자의 MaxHp만큼 현재 체력을 설정하게 해놓음
    // ㄴ start에서 
    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();

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

        if (monster_type != MONSTER_TYPE.BOSS_MONSTER)
        {
            hitState = new EnemyHitState(this);
        }
        else
        {
            hitState = null;
        }

        
    }
    protected void OnEnable()
    {
        monsterInfo = GetComponent<MonsterInfo>();

        monsterInfo._currentHP = monsterInfo._maxHP;

        _monsterOriginPosition = transform.position;

        _monsterOriginRotation = transform.rotation;

        currentState = idleState;
        currentState?.Enter();
    }


    protected virtual void Start()
    {
        PlayerObject = PlayerGameobjectManager.instance.playerObject;

    }

    // 플레이어나 몬스터가 참조할 수 있도록 public으로 
    // 보스 기준
    public virtual void Hit(float damage)
    {
        monsterInfo._currentHP -= damage;

    }

    private void Update()
    {
        currentState.Update();
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
}
public enum MONSTER_TYPE
{
    BOSS_MONSTER,
    MELEE_MONSTER,
    RANGED_MONSTER
}