using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : EnemyAttackState
{
    public RangedAttackState(MonsterController character) : base(character) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
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
