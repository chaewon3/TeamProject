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

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < mummies.Length; i++)
            {
                if (mummies[i] != null && mummies[i].activeInHierarchy)
                {
                    mummies[i].GetComponent<MonsterController>().CharacterGotOutArea();
                    mummies[i].GetComponent<MonsterController>().SetCharacterTransformNull();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            Gizmos.DrawWireCube(transform.position + boxCollider.center, boxCollider.size);
        }
    }
}
