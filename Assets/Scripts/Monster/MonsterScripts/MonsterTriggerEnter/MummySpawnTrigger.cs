using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummySpawnTrigger : MonoBehaviour
{
    public GameObject[] mummies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < mummies.Length; i++)
            {
                if (mummies[i] != null)
                {
                    mummies[i].SetActive(true);
                }
            }
        }
    }
}
