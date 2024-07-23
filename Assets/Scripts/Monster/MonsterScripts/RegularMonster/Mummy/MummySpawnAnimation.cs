using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummySpawnAnimation : MonoBehaviour
{
    public GameObject coffin;
    public GameObject mummy;

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


            Debug.DrawRay(rayOrigin, rayDirection * 0.4f, Color.red, 0.1f);


            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, 0.2f, groundLayer))
            {
                break;
            }

            yield return new WaitForSeconds(0.01f);
        }


        StartCoroutine(CoffinBackMoveAnimation());
    }

    IEnumerator CoffinBackMoveAnimation()
    {
        int moveBackTimes = 0;
        int time = 0;

        ColliderAndRigidBodyDisable(coffin);

        ColliderAndRigidBodyDisable(mummy);

        while (true)
        {
            moveBackTimes += 1;
            Vector3 backwardMovement = new Vector3(0, 0, -0.01f);
            coffin.transform.localPosition += backwardMovement;

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


        StartCoroutine(MummyOpenAnimation());
    }

    IEnumerator MummyOpenAnimation()
    {
        Animator animator = GetComponentInChildren<Animator>();
        animator.SetTrigger("open");
        yield return new WaitForSeconds(4.0f);

        ColliderAndRigidBodyEnable(mummy);

        MonsterController monsterController = GetComponentInChildren<MonsterController>();
        monsterController._characterGotIntoArea = true;
    }

    void ColliderAndRigidBodyDisable(GameObject target)
    {
        Collider collider = target.GetComponent<Collider>();
        Rigidbody rigidbody = target.GetComponent<Rigidbody>();

        if (collider != null)
        {
            collider.enabled = false;
        }
        if (rigidbody != null)
        {
            rigidbody.isKinematic = true;
        }
    }

    void ColliderAndRigidBodyEnable(GameObject target)
    {
        Collider collider = target.GetComponent<Collider>();
        Rigidbody rigidbody = target.GetComponent<Rigidbody>();

        if (collider != null)
        {
            collider.enabled = true;
        }
        if (rigidbody != null)
        {
            rigidbody.isKinematic = false;
        }
    }

}
