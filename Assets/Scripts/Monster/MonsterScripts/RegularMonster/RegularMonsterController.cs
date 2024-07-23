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

        // 맞았을 때 움직임으로 바꾸게
        // 처음부터 몬스터는 캐릭터의 오브젝트를 참조는 하고 있다가
        // 맞았을 때 움직일 때 트랜스폼을 참조한다.


        TransitionToState(moveState);

        monsterInfo._IsAttacked = true;
    }



}