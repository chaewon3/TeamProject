using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : EnemyAttackState
{
    public BossAttackState(MonsterController character) : base(character) { }

    static readonly int PatternAttack = Animator.StringToHash("PatternAttack");
    static readonly int PatternSpellOne = Animator.StringToHash("PatternSpellOne");
    static readonly int PatternSpellTwoCharging = Animator.StringToHash("PatternSpellTwoCharging");
    static readonly int PatternSpellTwoShot = Animator.StringToHash("PatternSpellTwoShot");

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
        BOSS_MONSTER_ATTACK_BEHAVIOUR bossPattern = (BOSS_MONSTER_ATTACK_BEHAVIOUR)@enum;

        bossPattern = BOSS_MONSTER_ATTACK_BEHAVIOUR.BOSS_MONSTER_SKILL_2;

        

        switch (bossPattern)
        {
            case BOSS_MONSTER_ATTACK_BEHAVIOUR.BOSS_MONSTER_ATTACK:
                monsterController.StartCoroutine(BossPatternAttack());
                break;
            case BOSS_MONSTER_ATTACK_BEHAVIOUR.BOSS_MONSTER_SKILL_1:
                monsterController.StartCoroutine(BossPatternSpellOne());
                break;
            case BOSS_MONSTER_ATTACK_BEHAVIOUR.BOSS_MONSTER_SKILL_2:
                monsterController.StartCoroutine(BossPatternSpellTwo());
                break;
            default:
                monsterController.TransitionToState(monsterController.idleState);
                break;
        }
    }

    IEnumerator BossPatternAttack()
    {
        monsterController.animator.SetTrigger(PatternAttack);
        yield return new WaitForSeconds(1f);
        monsterController.TransitionToState(monsterController.idleState);
    }

    IEnumerator BossPatternSpellOne()
    {
        monsterController.animator.SetTrigger(PatternSpellOne);
        yield return new WaitForSeconds(2f);
        monsterController.TransitionToState(monsterController.idleState);
    }

    IEnumerator BossPatternSpellOneCooltime(System.Enum @enum)
    {
        monsterController.monsterInfo._monsterBehaviourPool[@enum] = false;
        yield return new WaitForSeconds(10f);
        monsterController.monsterInfo._monsterBehaviourPool[@enum] = true;
    }

    IEnumerator BossPatternSpellTwo()
    {
        monsterController.animator.SetBool(PatternSpellTwoCharging, true);

        yield return new WaitForSeconds(3f);

        monsterController.animator.SetTrigger(PatternSpellTwoShot);
        
        yield return new WaitForSeconds(1.1f);
        monsterController.TransitionToState(monsterController.idleState);
    }


}
