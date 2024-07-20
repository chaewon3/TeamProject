using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAttackState : EnemyState

{
    public EnemyAttackState(MonsterController character) : base(character) { }

    static readonly int InPattern = Animator.StringToHash("InPattern");

    public override void Enter()
    {
        monsterController.animator.SetBool(InPattern, true);

        monsterController.animator.applyRootMotion = false;

        PatternCooltime(SelectPattern());
    }
    public override void Update()
    {
        
    }

    public override void Exit()
    {
        monsterController.animator.SetBool(InPattern, false);

        monsterController.animator.applyRootMotion = true;
    }

    Enum SelectPattern()
    {
        monsterController.patturnIndexes = new List<Enum>();
        Enum randomKey = null;

        foreach (var behaviour in monsterController.monsterInfo._monsterBehaviourPool)
        {
            monsterController.patturnIndexes.Add(behaviour.Key);
        }

        if (monsterController.patturnIndexes.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, monsterController.patturnIndexes.Count);
            randomKey = monsterController.patturnIndexes[randomIndex];
            
            return randomKey;
        }

        return null;
    }

    protected virtual void PatternCooltime(Enum @enum)
    {
        
    }
}
