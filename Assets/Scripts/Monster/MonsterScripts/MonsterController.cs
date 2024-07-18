using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterInfo))]
public abstract class MonsterController : MonoBehaviour, IHitable
{

    protected MonsterInfo monsterInfo;

    Coroutine _rotationCoroutine;

    // _characterGotIntoArea�� true�� �ǰ�, _isMove�� true �� �� ��
    // ������ �� �ְ� ���
    public bool _isMove = false;

    public bool _doingSomeAction = false;

    // �� ó���� ���� �����ϰ�
    //protected Transform _monsterOriginTransform;
    // ��� ���� ��ӹ��� �ְ� �ǵ� �ʿ���� �� ����
    Vector3 _monsterOriginPosition;
    Quaternion _monsterOriginRotation;

    EnemyState currentState;


    // trigger�� ������ �� ()
    // ĳ���� ��ġ�� null �϶� ���� �ڸ��� ���ư��� ¥���� ��
    // ��׵� ���� ��ӹ��� �ְ� �ǵ� �ʿ���� �� ����
    public bool _characterGotIntoArea = false;
    public GameObject PlayerObject;
    Transform _characterTransfrom;

    public Animator animator;

    public EnemyIdleState idleState;
    public EnemyMoveState moveState;
    public EnemyAttackState attackState;



    // ��ӹ��� ��ũ��Ʈ���� �������� �� �� �ϰ� base.Awake() �ؾ� ��
    // ������ ��ġ�� ������ MaxHp��ŭ ���� ü���� �����ϰ� �س���
    // �� start���� 
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

    // �÷��̾ ���Ͱ� ������ �� �ֵ��� public���� 
    public void Hit(float damage)
    {
        monsterInfo._currentHP -= damage;
    }

    // ������ ü�¿� ������ ��� �ϴµ� �Լ� ��ü�� ���� �ָ��ؼ� ����
    void Attack()
    {

    }


    // Start �ڷ�ƾ���� move�� ���ο� ��ų�̳� ������ �� ������  ���ϰ� update�� ĳ������ ��ġ�� null�� �ƴϰ� move�� true�� �� �����̰�
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
            // ĳ���� Ʈ�������� ã�� �����̴� �Լ�
            // �ڱ� �ڸ��� ���ư� ���� _isMove�� true�̰� null�϶��� �Ϸ� ������
            // �׳� ��� ã���� �� �����Ӹ� �����ϰ� start���� 1�ʸ��� ã�� �� ĳ������ ��ġ�� null�̸�
            // ���� �ڸ��� ���� �Լ��� �����ϴ� ���� �� ���ƺ���
            // �׷� null�� ���⼭ �Ź� üũ�� ���ص� �� �� ����
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
                // && (Vector3.SqrMagnitude(transform.position - _monsterOriginPosition) > 1) �� ������ ���ԵǾ�� �ϳ�

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


        // ��Ÿ Ÿ�� �տ� 1�� speed�� 
        transform.position = Vector3.MoveTowards(currentPosition, new Vector3(targetPosition.x, currentPosition.y, targetPosition.z), monsterInfo._moveSpeed * Time.deltaTime);

        if (Vector3.SqrMagnitude(currentPosition - targetPosition) <= (stopRange * stopRange))
        {
            // ���⼭ isMove = false�� ���� �ʰ� ��ų�̳� ���� �ʿ��� �ش� �ִϸ��̼ǵ��� false�� �ٲٱ⸸ �ϸ� �� ��

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

    // trigger Enter���� ȣ���� �Լ�
    // �� ���� �� ��ó�� �� �濡�� ��� Ȱ��ȭ ��Ű�� �����
    public void CharacterGotIntoArea()
    {
        _characterGotIntoArea = true;

        // �����ȿ� ���Դٸ� �ڷ�ƾ�� ��� ����
        // ���� Ȱ��ȭ ����
        StartCoroutine(OnCharaterEnterArea());
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
        PlayerObject = targetObject;
    }

    // ĳ���� ��ġ�� �ҷ����� �Լ�
    public void LoadCharacterTransform()
    {
        _characterTransfrom = PlayerObject.transform;

        _isMove = true;

        animator.SetBool("IsMove", true);
    }

    // ĳ���Ͱ� Ʈ���� ������ ������ �� ȣ���� �Լ�
    // �ڽ��� ��ġ���� �̵��Ѵ�.
    // bool �����ϳ� �� ����� update���� ó���ϱ�
    // �׳� intoArea�� false�� ����ǰ� �ϴ°� �´°� �ƴѰ�
    // ���� �̰� �ʿ� ���� false�� �� target�� ������ origin���� 
    // �ʿ���� �ʰ� exit�Ҷ� �ѹ� ȣ���ؼ� ����ϸ� �� ��
    public void SetCharacterTransformNull()
    {
        // �̰� null�� �� ���͵��� �ڽ��� ���� ��ġ�� �̵��ϰ� �����
        // �ٵ� ���� null�� �ٲپ�� �ϳ� 
        // �ٲپ�� �Ѵ� �޸𸮰� �����ֱ� ������ ������ �÷����� ���� �޸𸮸� ȸ���� �;� ��

        PlayerObject = null;
        _characterTransfrom = null;
        //_isMove = false;

    }

    // ��� �޾Ƽ� ���� ���ڿ� ���� �Լ� �����ϰ� �����
    // ��� ���ڸ� �̴°Ŷ� ���� virtual�̾�� �� ������ ���� ����

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

            // �켱 ĳ���Ͱ� ���� �ȿ� ���� �ֳ� üũ
            if (_characterGotIntoArea)
            {
                // ĳ���� ��ġ �޾ƿ���

                if (PlayerObject != null && !_doingSomeAction)
                {
                    LoadCharacterTransform();
                }

                // isMove�� Ȱ��ȭ�Ǿ� �ֳ� üũ
                if (_isMove)
                {
                    // �����̰� �ִٸ� 1�� �� ��ġ ã���� ����
                    print("Searching Character");

                    yield return new WaitForSeconds(1.0f);
                }
                else
                {
                    print("skill or attack");
                    // ���⼭ ��ų üũ
                    yield return new WaitForSeconds(5.0f);
                }


            }
            else
            {
                // �̰��϶� ���� ������ ����
                //print($"_characterGotIntoArea : {_characterGotIntoArea}");
                yield return new WaitForSeconds(1.0f);
            }
            
        }
    }

    // ȸ���ϴ� �ڷ�ƾ
    // ���ڸ����� �ɾ ȸ���ϰ� ������ ��
    // Ȥ�� ���� IK�� �����ϰ� �ִϸ��̼� �缳��
    // Ȥ�� �׳� ����ϰ� ȸ���ϱ�
    // �ȴ� �ִϸ��̼� Ȱ��ȭ �� 
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
            yield return null; // �� �����Ӹ��� ���
        }
    }
}
