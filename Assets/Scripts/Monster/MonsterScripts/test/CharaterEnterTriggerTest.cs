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
                aa[i].GetComponent<MonsterController>().LoadCharacterObject(other.gameObject);
                aa[i].GetComponent<MonsterController>().CharacterGotIntoArea();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //print("Player Out");

            for (int i = 0; i < aa.Length; i++)
            {

                aa[i].GetComponent<MonsterController>().CharacterGotOutArea();
                aa[i].GetComponent<MonsterController>().SetCharacterTransformNull();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // Gizmo의 색상을 설정합니다.

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            // Box Collider의 중심과 크기를 가져와서 Gizmo로 그립니다.
            Gizmos.DrawWireCube(transform.position + boxCollider.center, boxCollider.size);
        }
    }

}
