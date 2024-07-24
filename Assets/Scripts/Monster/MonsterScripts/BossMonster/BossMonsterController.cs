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

        if (bossHPBar == null)
        {
            print(11);
        }

        if (CanvasManager.Instance.BossHPBar == null)
        {
            print(22);
        
        }
        else
        {
            print(33);
        }

        //bossHPBar = CanvasManager.Instance.BossHPBar;
    }


    public override void Hit(float damage)
    {
        base.Hit(damage);

        if (bossHPBar == null)
            print("���� ���� �����");
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

        // �¾��� �� ���������� �ٲٰ�
        // ó������ ���ʹ� ĳ������ ������Ʈ�� ������ �ϰ� �ִٰ�
        // �¾��� �� ������ �� Ʈ�������� �����Ѵ�.
        
        monsterInfo._IsAttacked = true;
    }
}