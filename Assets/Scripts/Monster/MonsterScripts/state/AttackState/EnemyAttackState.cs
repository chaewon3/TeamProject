using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAttackState : EnemyState

{
    public EnemyAttackState(MonsterController character) : base(character) { }

    public override void Enter()
    {
        SelectPattern();

    }

    // 업데이트에선 아무것도 안하고 애니메이션에 있는 콜라이더를 활성화 해서 공격하기?
    // 딱히 업데이트에서 할 건 없어보임
    public override void Update()
    {
        
    }

    public override void Exit()
    {
        // 상태 종료 시의 처리
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
            monsterController.monsterInfo._monsterBehaviourPool[randomKey] = false;
            return randomKey;
        }

        return null;
    }

    protected virtual void PatternCooltime(Enum @enum)
    {
        
    }
}
