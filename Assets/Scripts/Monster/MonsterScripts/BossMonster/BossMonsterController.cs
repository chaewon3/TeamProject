using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterController : MonsterController
{
    UnityEngine.UI.Slider bossHPBar;

    protected override void Start()
    {
        base.Start();
        bossHPBar = CanvasManager.Instance.BossHPBar.GetComponent<UnityEngine.UI.Slider>();


        //bossHPBar = CanvasManager.Instance.BossHPBar;
    }


    public override void Hit(float damage)
    {
        base.Hit(damage);

        if (bossHPBar == null)
            print("보스 피통 비었음");
        else
            bossHPBar.value = monsterInfo._currentHP / monsterInfo._maxHP;

        if (monsterInfo._currentHP <= 0)
        {
            if (!_isDead)
            {
                TransitionToState(deadState);
            }
            return;

        }

        // 맞았을 때 움직임으로 바꾸게
        // 처음부터 몬스터는 캐릭터의 오브젝트를 참조는 하고 있다가
        // 맞았을 때 움직일 때 트랜스폼을 참조한다.
        
        monsterInfo._IsAttacked = true;
    }
}