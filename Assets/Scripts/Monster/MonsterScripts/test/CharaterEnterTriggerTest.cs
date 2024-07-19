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
            print("playerin");
            for (int i = 0; i < aa.Length; i++)
            {
                aa[i].GetComponent<MonsterController>().LoadCharacterObject(other.gameObject);
                aa[i].GetComponent<MonsterController>().CharacterGotIntoArea();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Player Out");

            for (int i = 0; i < aa.Length; i++)
            {

                aa[i].GetComponent<MonsterController>().CharacterGotOutArea();
                aa[i].GetComponent<MonsterController>().SetCharacterTransformNull();
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
