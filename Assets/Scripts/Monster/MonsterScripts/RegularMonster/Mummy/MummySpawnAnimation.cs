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
    Animator animator;

    MonsterController monsterController;


    private void Awake()
    {
        groundLayer = LayerMask.GetMask("Ground");
        animator = GetComponentInChildren<Animator>();
        monsterController = GetComponentInChildren<MonsterController>();
    }

    private void OnEnable()
    {
        animator.SetBool("IsActivating", true);
        StartCoroutine(RaycastingAndStartAnim());

        monsterController._characterGotIntoArea = false;
        
        InitializingObject(coffin);
        InitializingObject(mummy);

        coffin.GetComponent<Rigidbody>().isKinematic = false;

        ColliderAndRigidBodyEnable(mummy);
    }

    IEnumerator RaycastingAndStartAnim()
    {
        while (true)
        {
            rayOrigin = coffin.transform.position;
            rayDirection = Vector3.down;


            Debug.DrawRay(rayOrigin, rayDirection * 0.4f, Color.red, 0.1f);


            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, 0.3f, groundLayer))
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

        coffin.SetActive(false);
        
        StartCoroutine(MummyOpenAnimation());
    }

    IEnumerator MummyOpenAnimation()
    {
        
        animator.SetTrigger("open");
        animator.SetBool("IsActivating", false);
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

    void InitializingObject(GameObject target)
    {
        target.transform.localPosition = Vector3.zero;
        target.transform.localEulerAngles = Vector3.zero;
        target.GetComponent<Rigidbody>().velocity = Vector3.zero;

        target.SetActive(true);
    }

}
