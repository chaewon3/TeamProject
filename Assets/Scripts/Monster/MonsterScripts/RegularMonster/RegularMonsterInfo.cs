using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularMonsterInfo : MonsterInfo
{


    protected override void Awake()
    {
        base.Awake();
        // °ø°Ý¸¸?
        _monsterBehaviourPool = new bool[] { true };
    }
}
