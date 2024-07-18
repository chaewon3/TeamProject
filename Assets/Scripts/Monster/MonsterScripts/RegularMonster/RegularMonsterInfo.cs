using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularMonsterInfo : MonsterInfo
{
    protected override void Awake()
    {
        base.Awake();
    }

    void InitializeBehaviourPool()
    {
        _monsterBehaviourPool[REGULAR_MONSTER_ATTACK_BEHAVIOUR.REGULAR_MONSTER_ATTACK] = true;
    }
}


public enum REGULAR_MONSTER_ATTACK_BEHAVIOUR
{
    REGULAR_MONSTER_ATTACK
}