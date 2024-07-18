using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterInfo : MonsterInfo
{


    protected override void Awake()
    {
        base.Awake();

        InitializeBehaviourPool();
    }

    void InitializeBehaviourPool()
    {
        _monsterBehaviourPool[BOSS_MONSTER_ATTACK_BEHAVIOUR.BOSS_MONSTER_ATTACK] = true;
        _monsterBehaviourPool[BOSS_MONSTER_ATTACK_BEHAVIOUR.BOSS_MONSTER_SKILL_1] = true;
        _monsterBehaviourPool[BOSS_MONSTER_ATTACK_BEHAVIOUR.BOSS_MONSTER_SKILL_2] = true;
    }

}

public enum BOSS_MONSTER_ATTACK_BEHAVIOUR
{
    BOSS_MONSTER_ATTACK,
    BOSS_MONSTER_SKILL_1,
    BOSS_MONSTER_SKILL_2
}
