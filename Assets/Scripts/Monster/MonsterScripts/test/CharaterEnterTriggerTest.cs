using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterEnterTriggerTest : MonoBehaviour
{
    public GameObject aa;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //print("Player In");
            aa.GetComponent<MonsterController>().LoadCharacterObject(other.gameObject);
            aa.GetComponent<MonsterController>().CharacterGotIntoArea();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //print("Player Out");
            aa.GetComponent<MonsterController>().CharacterGotOutArea();
            aa.GetComponent<MonsterController>().SetCharacterTransformNull();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // Gizmo�� ������ �����մϴ�.

        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            // Box Collider�� �߽ɰ� ũ�⸦ �����ͼ� Gizmo�� �׸��ϴ�.
            Gizmos.DrawWireCube(transform.position + boxCollider.center, boxCollider.size);
        }
    }

}
