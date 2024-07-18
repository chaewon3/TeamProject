using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    //public EnemyIdleState(MonsterController character) : base(monsterState)
    //{
    //}

    public override void Enter()
    {
        monsterState.animator.SetBool("IsMove", false);
    }

    public override void Exit()
    {
        if (monsterState._characterGotIntoArea)
        {
            monsterState.TransitionToState(monsterState.moveState);
        }
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
