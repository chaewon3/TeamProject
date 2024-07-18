using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : EnemyAttackState
{
    public BossAttackState(MonsterController character) : base(character) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (!monsterController._characterGotIntoArea)
        {
            monsterController.TransitionToState(monsterController.idleState);
        }
    }

    public override void Exit()
    {
        // 상태 종료 시의 처리
    }

    protected override void PatternCooltime(System.Enum @enum)
    {
        BOSS_MONSTER_ATTACK_BEHAVIOUR bossPattern = (BOSS_MONSTER_ATTACK_BEHAVIOUR)@enum;

        switch (bossPattern)
        {
            case BOSS_MONSTER_ATTACK_BEHAVIOUR.BOSS_MONSTER_ATTACK:
                break;
            case BOSS_MONSTER_ATTACK_BEHAVIOUR.BOSS_MONSTER_SKILL_1:
                break;
            case BOSS_MONSTER_ATTACK_BEHAVIOUR.BOSS_MONSTER_SKILL_2:
                break;
            default:
                break;
        }
    }
}
