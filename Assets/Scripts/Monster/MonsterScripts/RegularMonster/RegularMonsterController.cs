using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularMonsterController : MonsterController
{
    public override void Hit(float damage)
    {
        base.Hit(damage);

        if (monsterInfo._currentHP <= 0)
        {
            if (!_isDead)
            {
                TransitionToState(deadState);
            }
            return;
        }
        else
        {
            TransitionToState(hitState);
        }

        // �¾��� �� ���������� �ٲٰ�
        // ó������ ���ʹ� ĳ������ ������Ʈ�� ������ �ϰ� �ִٰ�
        // �¾��� �� ������ �� Ʈ�������� �����Ѵ�.


        TransitionToState(moveState);

        monsterInfo._IsAttacked = true;
    }



}