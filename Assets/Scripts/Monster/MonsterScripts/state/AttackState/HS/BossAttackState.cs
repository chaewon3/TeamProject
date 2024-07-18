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

    private IEnumerator Attack()
    {
        while (true)
        {

            // 공격 로직
            yield return new WaitForSeconds(5.0f);
        }
    }
}
