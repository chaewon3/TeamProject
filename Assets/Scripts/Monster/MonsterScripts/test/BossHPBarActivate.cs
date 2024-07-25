using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPBarActivate : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //CanvasManager.Instance.ShowBossHPBar();
        }
    }
}
