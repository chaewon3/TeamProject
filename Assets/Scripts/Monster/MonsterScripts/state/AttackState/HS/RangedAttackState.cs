using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : EnemyAttackState
{
    public RangedAttackState(MonsterController character) : base(character) { }

    static readonly int PatternAttack = Animator.StringToHash("PatternAttack");

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
                monsterController.StartCoroutine(RegularPatternAttack());
                break;
            default:
                break;
        }
    }

    IEnumerator RegularPatternAttack()
    {
        monsterController.animator.SetTrigger(PatternAttack);
        yield return new WaitForSeconds(2.5f);

        if (!monsterController._isHit && !monsterController._isDead)
        {
            monsterController.TransitionToState(monsterController.moveState);
        }
    }
}
