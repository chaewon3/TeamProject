using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterInfo : MonsterInfo
{


    protected override void Awake()
    {
        // �ൿ�� ���� ��ŭ (����1, ����2, ��ų1 ��)(�����̴� �ൿ�� ���⿡ ���� ���� ���� �Ÿ� �̻� �־����� ���� ������ �̵�?) �迭 �ε��� ���� ����
        _monsterBehaviourPool = new int[] { 0, 0, 0, 0, 0 };

    }


}
