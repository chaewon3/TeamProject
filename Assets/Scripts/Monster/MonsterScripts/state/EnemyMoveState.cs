using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public EnemyMoveState(MonsterController character) : base(character) { }

    public override void Enter()
    {
        monsterState.animator.SetBool("IsMove", true);
    }

    public override void Update()
    {
        if (monsterState.PlayerObject != null && !monsterState._doingSomeAction)
        {
            monsterState.LoadCharacterTransform();
        }

        if (monsterState._isMove)
        {
            monsterState.StartCoroutine(SearchCharacter());
        }
        else
        {
            monsterState.TransitionToState(monsterState.attackState);
        }
    }

    public override void Exit()
    {
        // ���� ���� ���� ó��
    }

    private IEnumerator SearchCharacter()
    {
        while (monsterState._isMove)
        {
            yield return new WaitForSeconds(1.0f);
            // ĳ���� ��ġ�� ã�� ����
        }
    }
}
