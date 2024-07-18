using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : EnemyAttackState
{
    public MeleeAttackState(MonsterController character) : base(character) { }

    public override void Enter()
    {
        print("Bossattackstate");
        monsterController.StartCoroutine(Attack());
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
