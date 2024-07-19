using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(MonsterController character) : base(character)
    {
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        if (monsterController._characterGotIntoArea || monsterController.monsterInfo._IsAttacked)
        {
            monsterController.TransitionToState(monsterController.moveState);
        }
    }
}
