using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTakeDamage : MonoBehaviour
{
    public GameObject aa;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            aa.GetComponent<MonsterController>().Hit(10);
        }
    }
}
