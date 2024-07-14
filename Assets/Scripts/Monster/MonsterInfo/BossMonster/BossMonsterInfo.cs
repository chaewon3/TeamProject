using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterInfo : MonsterInfo
{


    protected override void Awake()
    {
        // 행동의 개수 만큼 (공격1, 공격2, 스킬1 등)(움직이는 행동은 여기에 넣지 말고 일정 거리 이상 멀어졌을 때는 무조건 이동?) 배열 인덱스 개수 설정
        _monsterBehaviourPool = new int[] { 0, 0, 0, 0, 0 };

    }


}
