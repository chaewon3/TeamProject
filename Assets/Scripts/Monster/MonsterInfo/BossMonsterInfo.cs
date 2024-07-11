using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterInfo : MonsterInfo
{

    #region ���͵��� ������ �ν����ͼ� �����ϱ� ���� public����

    public float bossMaxHp = 100;
    public float bossAttackDamage = 10;


    #endregion


    protected override void Awake()
    {
        _maxHP = bossMaxHp;

        _attackDamage = bossAttackDamage;

        // �ൿ�� ���� ��ŭ (����1, ����2, ��ų1 ��)(�����̴� �ൿ�� ���⿡ ���� ���� ���� �Ÿ� �̻� �־����� ���� ������ �̵�?) �迭 �ε��� ���� ����
        _monsterBehaviourPool = new int[] { 0, 0, 0, 0, 0 };

        base.Awake();

        print(_currentHP);
    }

    
}
