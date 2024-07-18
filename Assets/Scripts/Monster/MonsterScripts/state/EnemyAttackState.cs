using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(MonsterController character) : base(character) { }

    public override void Enter()
    {
        monsterState.StartCoroutine(Attack());
    }

    public override void Update()
    {
        if (!monsterState._characterGotIntoArea)
        {
            monsterState.TransitionToState(monsterState.idleState);
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
