using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterController : MonsterController
{

    //protected override IEnumerator doSomething(int index)
    //{
    //    _doingSomeAction = true;

    //    print(index);
    //    switch (index)
    //    {
    //        case 0:
    //            StartCoroutine(attack(index));
    //            break;
    //        case 1:
    //            StartCoroutine(attack(index)); 
    //            break;
    //        case 2:
    //            StartCoroutine(attack(index)); 
    //            break;
    //        case 3:
    //            StartCoroutine(attack(index)); 
    //            break;
    //        case 4:
    //            StartCoroutine(attack(index)); 
    //            break;
    //        default:
    //            print("other Something");
    //            break;
    //    }

    //    yield return new WaitForSeconds(2.0f);
    //    _doingSomeAction = false;
    //}


    //IEnumerator attack(int index)
    //{
    //    print($"{index}¹ø index");

    //    yield return new WaitForSeconds(2.0f);
    //    MonsterInfo.instance._monsterBehaviourPool[index] = true;

    //    print($"{index}¹ø index °ª{MonsterInfo.instance._monsterBehaviourPool[index]}");
    //}

}

public enum MonsterState
{
    ONATTACK = 0,
    ONSKILL = 1
}