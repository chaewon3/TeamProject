using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : EnemyAttackState
{
    public RangedAttackState(MonsterController character) : base(character) { }

    public override void Enter()
    {
        print("Bossattackstate");
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
        REGULAR_MONSTER_ATTACK_BEHAVIOUR regularPattern = (REGULAR_MONSTER_ATTACK_BEHAVIOUR)@enum;

        switch (regularPattern)
        {
            case REGULAR_MONSTER_ATTACK_BEHAVIOUR.REGULAR_MONSTER_ATTACK:
                break;
            default:
                break;
        }
    }
}
