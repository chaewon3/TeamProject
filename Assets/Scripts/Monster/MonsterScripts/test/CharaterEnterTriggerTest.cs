using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterEnterTriggerTest : MonoBehaviour
{
    public GameObject[] aa;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < aa.Length; i++)
            {
                if (aa[i] != null && aa[i].activeInHierarchy)
                {
                    aa[i].GetComponent<MonsterController>().LoadCharacterObject(other.gameObject);
                    aa[i].GetComponent<MonsterController>().CharacterGotIntoArea();
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < aa.Length; i++)
            {
                if (aa[i] != null && aa[i].activeInHierarchy)
                {
                    aa[i].GetComponent<MonsterController>().CharacterGotOutArea();
                    aa[i].GetComponent<MonsterController>().SetCharacterTransformNull();
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
