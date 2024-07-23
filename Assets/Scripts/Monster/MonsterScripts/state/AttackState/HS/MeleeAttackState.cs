using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : EnemyAttackState
{
    public MeleeAttackState(MonsterController character) : base(character) { }

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
                monsterController.StartCoroutine(RegularPatternAttack(monsterController.monsterInfo.attackDurationTime));
                break;
            default:
                break;
        }
    }

    IEnumerator RegularPatternAttack(float time)
    {
        print("regularattack");
        print(time);
        monsterController.animator.SetTrigger(PatternAttack);
        yield return new WaitForSeconds(time);
        monsterController.TransitionToState(monsterController.idleState);

    }
}
