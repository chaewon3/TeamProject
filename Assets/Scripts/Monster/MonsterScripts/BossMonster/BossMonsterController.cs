using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterController : MonsterController
{
    UnityEngine.UI.Slider bossHPBar;

    protected override void Start()
    {
        base.Start();
        bossHPBar = CanvasManager.Instance.BossHPBar.GetComponentInChildren<UnityEngine.UI.Slider>();

    }

    public override void Hit(float damage)
    {
        base.Hit(damage);

        bossHPBar.value = monsterInfo._currentHP / monsterInfo._maxHP;

        if (monsterInfo._currentHP <= 0)
        {
            if (!_isDead)
            {
                TransitionToState(deadState);
            }
            return;

        }
        monsterInfo._IsAttacked = true;
    }
}