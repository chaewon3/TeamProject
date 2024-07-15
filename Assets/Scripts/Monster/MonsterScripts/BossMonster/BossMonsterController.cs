using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterController : MonsterController
{

    protected override IEnumerator doSomething(int index)
    {
        _doingSomeAction = true;

        print(index);
        switch (index)
        {
            case 0:
                StartCoroutine(attack(index));

                break;
            case 1:
                StartCoroutine(attack(index)); 
                break;
            case 2:
                StartCoroutine(attack(index)); 
                break;
            case 3:
                StartCoroutine(attack(index)); 
                break;
            case 4:
                StartCoroutine(attack(index)); 
                break;
            default:
                print("other Something");
                break;
        }

        //switch (index)
        //{
        //    case (int)MonsterState.ONATTACK:
        //        break;
        //    case MonsterState.ONSKILL:
        //        break;
        //    default:
        //        break;
        //}



        yield return new WaitForSeconds(2.0f);
        _doingSomeAction = false;
    }


    IEnumerator attack(int index)
    {
        print($"{index}¹ø index");

        yield return new WaitForSeconds(2.0f);
        monsterInfo._monsterBehaviourPool[index] = 0;

        print($"{index}¹ø index °ª{monsterInfo._monsterBehaviourPool[index]}");
    }

}

public enum MonsterState
{
    ONATTACK = 0,
    ONSKILL = 1
}