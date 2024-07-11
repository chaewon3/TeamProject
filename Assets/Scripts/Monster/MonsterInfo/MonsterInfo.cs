using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterInfo : MonoBehaviour, IHitable
{
    // �ν����Ϳ��� ������ �� �ִ� ��
    protected float _maxHP;
    protected float _currentHP;
    protected float _attackDamage;


    // _characterGotIntoArea�� true�� �ǰ�, _isMove�� true �� �� ��
    // ������ �� �ְ� ���
    protected bool _isMove = false;

    // �� ó���� ���� �����ϰ�
    protected Transform _monsterOriginTransform;

    // trigger�� ������ �� ()
    // ĳ���� ��ġ�� null �϶� ���� �ڸ��� ���ư��� ¥���� ��
    protected bool _characterGotIntoArea = false;
    protected Transform _characterTransfrom;


    // ��ӹ޴� ���ͳ� ���� ���� ��ũ��Ʈ���� ������ (������ ������ŭ)
    protected int[] _monsterBehaviourPool;


    // ��ӹ��� ��ũ��Ʈ���� �������� �� �� �ϰ� base.Awake() �ؾ� ��
    // ������ ��ġ�� ������ MaxHp��ŭ ���� ü���� �����ϰ� �س���
    protected virtual void Awake()
    {
        _currentHP = _maxHP;
        _monsterOriginTransform = this.transform;
    }

    // �÷��̾ ���Ͱ� ������ �� �ֵ��� public���� 
    public void Hit(float damage)
    {
        _currentHP -= damage;
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
        
    }


    // trigger Enter���� ȣ���� �Լ�
    // �� ���� �� ��ó�� �� �濡�� ��� Ȱ��ȭ ��Ű�� �����
    public void CharacterGotIntoArea()
    {
        _characterGotIntoArea = true;
        StartCoroutine(SearchingCharacter());
    }

    // trigger Exit���� ȣ���� �Լ�
    // �� �濡�� ��� ��Ȱ��ȭ ��Ű�� 
    public void CharacterGotOutArea()
    {
        _characterGotIntoArea = false;

        // �̰� null�� �� ���͵��� �ڽ��� ���� ��ġ�� �̵��ϰ� �����
        _characterTransfrom = null;
        
    }
    

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

}
