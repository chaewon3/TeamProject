using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummySpawnAnimation : MonoBehaviour
{
    public GameObject coffin;
    Vector3 rayOrigin;
    Vector3 rayDirection;
    LayerMask groundLayer;

    private void Awake()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void OnEnable()
    {
        StartCoroutine(RaycastingAndStartAnim());
    }

    IEnumerator RaycastingAndStartAnim()
    {
        while (true)
        {
            rayOrigin = coffin.transform.position;
            rayDirection = Vector3.down;

            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, 0.5f, groundLayer))
            {
                break;
            }

            yield return new WaitForSeconds(0.25f);
        }


        StartCoroutine(CoffinBackMoveAnimation());
    }

    IEnumerator CoffinBackMoveAnimation()
    {
        int moveBackTimes = 0;
        int time = 0;

        Collider collider = coffin.GetComponent<Collider>();
        Rigidbody rigidbody = coffin.GetComponent<Rigidbody>();

        if (collider != null)
        {
            collider.enabled = false;
        }
        if (rigidbody != null)
        {
            rigidbody.isKinematic = true;
        }



        while (true)
        {
            moveBackTimes += 1;
            Vector3 backwardMovement = new Vector3(0, 0, -0.01f);
            coffin.transform.position += backwardMovement;

            yield return new WaitForSeconds(0.01f);

            if (moveBackTimes >= 50)
            {
                break;
            }
        }

        while (true)
        {
            time += 1;
            Vector3 downwardMovement = new Vector3(0, -0.03f, 0);

            coffin.transform.position += downwardMovement;
            yield return new WaitForSeconds(0.01f);

            if (time >= 100)
            {
                break;
            }
        }


        Destroy(coffin);


    }

}
