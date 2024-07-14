using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsterInfo))]
public abstract class MonsterController : MonoBehaviour, IHitable
{
    MonsterInfo monsterInfo;

    Coroutine _rotationCoroutine;

    // _characterGotIntoArea�� true�� �ǰ�, _isMove�� true �� �� ��
    // ������ �� �ְ� ���
    protected bool _isMove = false;

    // �� ó���� ���� �����ϰ�
    protected Transform _monsterOriginTransform;

    // trigger�� ������ �� ()
    // ĳ���� ��ġ�� null �϶� ���� �ڸ��� ���ư��� ¥���� ��
    protected bool _characterGotIntoArea = false;
    protected Transform _characterTransfrom;

    // ��ӹ��� ��ũ��Ʈ���� �������� �� �� �ϰ� base.Awake() �ؾ� ��
    // ������ ��ġ�� ������ MaxHp��ŭ ���� ü���� �����ϰ� �س���
    protected virtual void Awake()
    {
        monsterInfo = GetComponent<MonsterInfo>();

        monsterInfo._currentHP = monsterInfo._maxHP;
        _monsterOriginTransform = this.transform;
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

    // ��ŸƮ�� �ƴ� trigger enter�Ǿ��� �� Ȱ��ȭ�ϰ� ����� �� ���� �� ����
    // ��ŸƮ�� �ع����� �ָ��ִ� ��ü�� ������� �������� ���ư��� ���ɿ� ������ �� �� ����
    // �ָ� ������ ��Ȱ��ȭ�� ����ϸ� ������ ����
    private IEnumerator Start()
    {
        while (true)
        {
            // üũ�� �ؾ� �ϳ�? 
            // Ʈ���ſ��� ���� �� �����ϰ� ���� �� stop�Ұǵ�? 

            // �켱 ĳ���Ͱ� ���� �ȿ� ���� �ֳ� üũ
            if (_characterGotIntoArea)
            {
                // ĳ���� ��ġ �޾ƿ���
                LoadCharacterTransform();

                // isMove�� Ȱ��ȭ�Ǿ� �ֳ� üũ
                if (_isMove)
                {
                    // �����̰� �ִٸ� 1�� �� ��ġ ã���� ����
                    yield return new WaitForSeconds(1.0f);
                }
                else
                {
                    // ���⼭ ��ų üũ
                    yield return new WaitForSeconds(5.0f);
                }


            }
        }


    }


    private void Update()
    {
        if (_isMove && _characterGotIntoArea)
        {
            // ĳ���� Ʈ�������� ã�� �����̴� �Լ�
            // �ڱ� �ڸ��� ���ư� ���� _isMove�� true�̰� null�϶��� �Ϸ� ������
            // �׳� ��� ã���� �� �����Ӹ� �����ϰ� start���� 1�ʸ��� ã�� �� ĳ������ ��ġ�� null�̸�
            // ���� �ڸ��� ���� �Լ��� �����ϴ� ���� �� ���ƺ���
            // �׷� null�� ���⼭ �Ź� üũ�� ���ص� �� �� ����
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

        // ��Ÿ Ÿ�� �տ� 1�� speed�� 
        transform.position = Vector3.MoveTowards(currentPosition, new Vector3(targetPosition.x, currentPosition.y, targetPosition.z), 1 * Time.deltaTime);

        if (Vector3.SqrMagnitude(currentPosition - targetPosition) <= (stopRange * stopRange))
        {
            _isMove = false;
        }
    }

    // trigger Enter���� ȣ���� �Լ�
    // �� ���� �� ��ó�� �� �濡�� ��� Ȱ��ȭ ��Ű�� �����
    public void CharacterGotIntoArea()
    {
        _characterGotIntoArea = true;
        // �̰� �ƴ϶� ���� start�� �ڷ�ƾ�� ȣ��
        StartCoroutine(SearchingCharacter());
    }

    // trigger Exit���� ȣ���� �Լ�
    // �� �濡�� ��� ��Ȱ��ȭ ��Ű�� 
    public void CharacterGotOutArea()
    {
        _characterGotIntoArea = false;

        // ���� �ִ� transform�� null�� ���⼭ �������� 

    }


    // ĳ���� ��ġ�� �ҷ����� �Լ�
    void LoadCharacterTransform()
    {

    }

    // ĳ���Ͱ� Ʈ���� ������ ������ �� ȣ���� �Լ�
    // �ڽ��� ��ġ���� �̵��Ѵ�.
    // bool �����ϳ� �� ����� update���� ó���ϱ�
    // �׳� intoArea�� false�� ����ǰ� �ϴ°� �´°� �ƴѰ�
    // ���� �̰� �ʿ� ���� false�� �� target�� ������ origin���� 
    // �ʿ���� �ʰ� exit�Ҷ� �ѹ� ȣ���ؼ� ����ϸ� �� ��
    void SetCharacterTransformNull()
    {
        // �̰� null�� �� ���͵��� �ڽ��� ���� ��ġ�� �̵��ϰ� �����
        // �ٵ� ���� null�� �ٲپ�� �ϳ� 
        // �ٲپ�� �Ѵ� �޸𸮰� �����ֱ� ������ ������ �÷����� ���� �޸𸮸� ȸ���� �;� ��

        _characterTransfrom = null;
    }

    // ��� �޾Ƽ� ���� ���ڿ� ���� �Լ� �����ϰ� �����
    // ��� ���ڸ� �̴°Ŷ� ���� virtual�̾�� �� ������ ���� ����
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


    // ������� �ڵ� �� ������� ����
    // _characterGotIntoArea�� true�� �Ǿ��� �� �ѹ� ȣ��
    // ���� �Ŵ������� �÷��̾��� ��ġ�� �޾ƿ� �� �ֵ��� �ϰ�
    // �װ� 1�ʸ��� �ҷ�����
    IEnumerator SearchingCharacter()
    {
        while (_characterGotIntoArea)
        {
            yield return new WaitForSeconds(1.0f);

            // �뷫 �̷��� �ҷ��ͼ� ����ϱ�
            Transform loadedTransform = null;
            _characterTransfrom = loadedTransform;
        }

        // ���� ȣ����� ���
        yield return null;
    }

    // ȸ���ϴ� �ڷ�ƾ
    // ���ڸ����� �ɾ ȸ���ϰ� ������ ��
    // Ȥ�� ���� IK�� �����ϰ� �ִϸ��̼� �缳��
    // Ȥ�� �׳� ����ϰ� ȸ���ϱ�
    IEnumerator RotateWhenReturn()
    {
        while (true)
        {

            yield return null;
        }
    }
}
