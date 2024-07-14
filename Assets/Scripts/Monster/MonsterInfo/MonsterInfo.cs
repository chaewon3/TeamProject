using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterInfo : MonoBehaviour
{

    // 인스펙터에서 설정할 수 있는 거
    public float _maxHP;
    public float _currentHP;
    public float _attackDamage;

    public float _attackDetectRange;
    public float _returnStopRange;

    // 상속받는 몬스터나 보스 몬스터 스크립트에서 재정의 (패턴의 개수만큼)
    public int[] _monsterBehaviourPool;

    // 배열 설정
    protected virtual void Awake()
    {

    }


    
}
