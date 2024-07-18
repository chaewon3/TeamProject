using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState

{
    public EnemyAttackState(MonsterController character) : base(character) { }

    public override void Enter()
    {
        print("enemyattackstate");
        monsterController.StartCoroutine(Attack());
    }

    // ������Ʈ���� �ƹ��͵� ���ϰ� �ִϸ��̼ǿ� �ִ� �ݶ��̴��� Ȱ��ȭ �ؼ� �����ϱ�?
    // ���� ������Ʈ���� �� �� �����
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

    void SelectPattern()
    {
        monsterController.patturnIndexes = new List<int>();
        int randomIndex;

        for (int i = 0; i < monsterController.monsterInfo._monsterBehaviourPool.Length; ++i)
        {
            if (monsterController.monsterInfo._monsterBehaviourPool[i] == true)
            {
                monsterController.patturnIndexes.Add(i);
            }
        }

        if (monsterController.patturnIndexes.Count > 0)
        {
            randomIndex = monsterController.patturnIndexes[Random.Range(0, monsterController.patturnIndexes.Count)];
            monsterController.monsterInfo._monsterBehaviourPool[randomIndex] = false;
        }
    }
}
