using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterInfo : MonsterInfo
{


    protected override void Awake()
    {
        base.Awake();

        // �ൿ�� ���� ��ŭ (����1, ����2, ��ų1 ��)(�����̴� �ൿ�� ���⿡ ���� ���� ���� �Ÿ� �̻� �־����� ���� ������ �̵�?) �迭 �ε��� ���� ����
        _monsterBehaviourPool = new bool[] { true, true, true, true};

    }


}
