using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attckColliderEnableTest : MonoBehaviour
{

    bool alreadyAttack;
    int a;

    private void OnEnable()
    {
        a = 0;
    }

    private void FixedUpdate()
    {
        if (alreadyAttack)
        {
            //print($"name {1} time {Time.realtimeSinceStartup}");
            alreadyAttack = false;
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !alreadyAttack)
        {
            alreadyAttack = true;
            a += 1;
            
            if (a == 1)
            {
                print("a");
            }
            else
            {
                print($"name {1} time {Time.realtimeSinceStartup}");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        alreadyAttack = false;
    }
}
