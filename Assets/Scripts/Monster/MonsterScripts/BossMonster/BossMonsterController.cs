using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterController : MonsterController
{
    UnityEngine.UI.Slider bossHPBar;

    public GameObject ddd;
    protected override void Start()
    {
        base.Start();
        bossHPBar = ddd.GetComponentInChildren<UnityEngine.UI.Slider>();


        //bossHPBar = CanvasManager.Instance.BossHPBar;
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

        // �¾��� �� ���������� �ٲٰ�
        // ó������ ���ʹ� ĳ������ ������Ʈ�� ������ �ϰ� �ִٰ�
        // �¾��� �� ������ �� Ʈ�������� �����Ѵ�.
        
        monsterInfo._IsAttacked = true;
    }
}