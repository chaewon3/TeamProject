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
        // ���� ���� ���� ó��
    }

    private IEnumerator Attack()
    {
        while (true)
        {

            // ���� ����
            yield return new WaitForSeconds(5.0f);
        }
    }
}
