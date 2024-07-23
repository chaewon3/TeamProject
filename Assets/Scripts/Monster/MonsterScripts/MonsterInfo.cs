using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterInfo : MonoBehaviour
{
    public static MonsterInfo instance; 

    // �ν����Ϳ��� ������ �� �ִ� ��
    public float _maxHP;
    public float _currentHP;
    public float _attackDamage;
    public float _moveSpeed;

    public float _attackDetectRange;
    public float _returnStopRange;
    public float _MaxChasingRange;
    public float attackDurationTime;
    public float exp;


    public bool _IsAttacked;

    

    public Dictionary<System.Enum, bool> _monsterBehaviourPool;

    // �迭 ����
    protected virtual void Awake()
    {
        instance = this;
        _monsterBehaviourPool = new Dictionary<System.Enum, bool>();
    }
}