using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterInfo : MonoBehaviour
{

    // �ν����Ϳ��� ������ �� �ִ� ��
    public float _maxHP;
    public float _currentHP;
    public float _attackDamage;

    public float _attackDetectRange;
    public float _returnStopRange;

    // ��ӹ޴� ���ͳ� ���� ���� ��ũ��Ʈ���� ������ (������ ������ŭ)
    public int[] _monsterBehaviourPool;

    // �迭 ����
    protected virtual void Awake()
    {

    }


    
}
