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

    // _characterGotIntoArea�� true�� �ǰ�, _isMove�� true �� �� ��
    // ������ �� �ְ� ���
    public bool _isMove = false;
    public bool _isDead = false;

    public bool _doingSomeAction = false;

    // �� ó���� ���� �����ϰ�
    // ��� ���� ��ӹ��� �ְ� �ǵ� �ʿ���� �� ����
    public Vector3 _monsterOriginPosition;
    public Quaternion _monsterOriginRotation;

    
    // trigger�� ������ �� ()
    // ĳ���� ��ġ�� null �϶� ���� �ڸ��� ���ư��� ¥���� ��
    // ��׵� ���� ��ӹ��� �ְ� �ǵ� �ʿ���� �� ����
    public bool _characterGotIntoArea = false;
    public GameObject PlayerObject;
    public Transform _characterTransfrom;

    


    // State��
    // attack�� ������ ���� 1�� ��ų 1��(2������� ��ų�� �ϳ� ����) �Ϲ��� 1�� �߿� �̱�
    EnemyState currentState;

    public EnemyIdleState idleState;
    public EnemyMoveState moveState;
    public EnemyAttackState attackState;
    public EnemyDeadState deadState;



    // ��ӹ��� ��ũ��Ʈ���� �������� �� �� �ϰ� base.Awake() �ؾ� ��
    // ������ ��ġ�� ������ MaxHp��ŭ ���� ü���� �����ϰ� �س���
    // �� start���� 
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

    // �÷��̾ ���Ͱ� ������ �� �ֵ��� public���� 
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

        // �¾��� �� ���������� �ٲٰ�
        // ó������ ���ʹ� ĳ������ ������Ʈ�� ������ �ϰ� �ִٰ�
        // �¾��� �� ������ �� Ʈ�������� �����Ѵ�.
        if (currentState == idleState)
        {
            //print(currentState);
            TransitionToState(moveState);

            monsterInfo._IsAttacked = true;
        }

    }

    // ������ ü�¿� ������ ��� �ϴµ� �Լ� ��ü�� ���� �ָ��ؼ� ����
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

    // trigger Enter���� ȣ���� �Լ�
    // �� ���� �� ��ó�� �� �濡�� ��� Ȱ��ȭ ��Ű�� �����
    public void CharacterGotIntoArea()
    {
        _characterGotIntoArea = true;
    }

    // trigger Exit���� ȣ���� �Լ�
    // �� �濡�� ��� ��Ȱ��ȭ ��Ű�� 
    public void CharacterGotOutArea()
    {
        _characterGotIntoArea = false;

    }


    // ���� �ȿ� ���Դٸ� ĳ���� ������Ʈ�� �����ϰ� �����
    // loadTransform���� ������Ʈ�� ���� ��ġ�� �����ϰ� ����
    public void LoadCharacterObject(GameObject targetObject)
    {
        if (PlayerObject == null)
        {
            PlayerObject = targetObject;
        }
        
    }

    // ĳ���� ��ġ�� �ҷ����� �Լ�
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
    // ȸ���ϴ� �ڷ�ƾ
    // ���ڸ����� �ɾ ȸ���ϰ� ������ ��
    // Ȥ�� ���� IK�� �����ϰ� �ִϸ��̼� �缳��
    // Ȥ�� �׳� ����ϰ� ȸ���ϱ�
    // �ȴ� �ִϸ��̼� Ȱ��ȭ �� 
    // ȸ���� ���ص� �� �� ���⵵
    //IEnumerator RotateWhenReturn()
    //{
    //    while (true)
    //    {
    //        if (Quaternion.Angle(transform.rotation, _monsterOriginRotation) < 10.0f)
    //        {
    //            yield break;
    //        }

    //        transform.rotation = Quaternion.Slerp(transform.rotation, _monsterOriginRotation, Time.deltaTime * 3.0f);
    //        yield return null; // �� �����Ӹ��� ���
    //    }
    //}
}
public enum MONSTER_TYPE
{
    BOSS_MONSTER,
    MELEE_MONSTER,
    RANGED_MONSTER
}