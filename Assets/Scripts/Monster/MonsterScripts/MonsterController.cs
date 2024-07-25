using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterInfo))]
public abstract class MonsterController : MonoBehaviour, IHitable
{
    public MonsterInfo monsterInfo;

    public Animator animator;

    public MONSTER_TYPE monster_type;

    public List<System.Enum> patturnIndexes;

    public bool _isMove = false;
    public bool _isDead = false;
    public bool _isHit = false;

    public Vector3 _monsterOriginPosition;
    public Quaternion _monsterOriginRotation;

    public bool _characterGotIntoArea = false;
    public GameObject PlayerObject;
    public Transform _characterTransfrom;

    public EnemyState currentState;

    public EnemyIdleState idleState;
    public EnemyMoveState moveState;
    public EnemyAttackState attackState;
    public EnemyDeadState deadState;
    public EnemyHitState hitState;

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

    public void CharacterGotIntoArea()
    {
        _characterGotIntoArea = true;
    }

    public void CharacterGotOutArea()
    {
        _characterGotIntoArea = false;

    }

    public void LoadCharacterObject(GameObject targetObject)
    {
        if (PlayerObject == null)
        {
            PlayerObject = targetObject;
        }
        
    }

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