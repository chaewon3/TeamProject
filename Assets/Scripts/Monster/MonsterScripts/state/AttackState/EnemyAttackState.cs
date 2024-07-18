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

    // ������Ʈ���� �ƹ��͵� ���ϰ� �ִϸ��̼ǿ� �ִ� �ݶ��̴��� Ȱ��ȭ �ؼ� �����ϱ�?
    // ���� ������Ʈ���� �� �� �����
    public override void Update()
    {
        
    }

    public override void Exit()
    {
        // ���� ���� ���� ó��
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
