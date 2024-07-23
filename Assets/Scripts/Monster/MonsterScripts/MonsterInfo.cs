using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterInfo : MonoBehaviour
{
    public static MonsterInfo instance; 

    // 인스펙터에서 설정할 수 있는 거
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

    // 배열 설정
    protected virtual void Awake()
    {
        instance = this;
        _monsterBehaviourPool = new Dictionary<System.Enum, bool>();
    }
}