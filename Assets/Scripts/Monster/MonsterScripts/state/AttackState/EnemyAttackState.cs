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

    // 업데이트에선 아무것도 안하고 애니메이션에 있는 콜라이더를 활성화 해서 공격하기?
    // 딱히 업데이트에서 할 건 없어보임
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
