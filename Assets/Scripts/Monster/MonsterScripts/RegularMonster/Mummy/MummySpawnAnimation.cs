using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummySpawnAnimation : MonoBehaviour
{
    public GameObject coffin;
    public GameObject mummy;

    public AudioClip audioClip;
    AudioSource audioSource;


    Vector3 rayOrigin;
    Vector3 rayDirection;
    public LayerMask groundLayer;
    


    Animator animator;
    static readonly int IsActivating = Animator.StringToHash("IsActivating");
    static readonly int open = Animator.StringToHash("open");


    MonsterController monsterController;

    Rigidbody coffinRigidbody;
    Rigidbody mummyRigidbody;

    Collider coffinCollider;
    Collider mummyCollider;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        animator = GetComponentInChildren<Animator>();
        monsterController = GetComponentInChildren<MonsterController>();

        coffinRigidbody = coffin.GetComponent<Rigidbody>();
        mummyRigidbody = mummy.GetComponent<Rigidbody>();

        coffinCollider = coffin.GetComponent<Collider>();
        mummyCollider = mummy.GetComponent<Collider>();

    }

    private void OnEnable()
    {
        animator.SetBool(IsActivating, true);
        StartCoroutine(RaycastingAndStartAnim());

        monsterController._characterGotIntoArea = false;
        
        InitializingObject(coffin);
        InitializingObject(mummy);


        OnGraivtyAndDisableCollider(coffinRigidbody, coffinCollider);
        OnGraivtyAndDisableCollider(mummyRigidbody, mummyCollider);
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
                audioSource.PlayOneShot(audioClip);

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

        coffinRigidbody.isKinematic = true;
        mummyRigidbody.isKinematic = true;


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
        
        animator.SetTrigger(open);
        animator.SetBool(IsActivating, false);
        yield return new WaitForSeconds(4.0f);

        mummyCollider.enabled = true;
        mummyRigidbody.isKinematic = false;

        MonsterController monsterController = GetComponentInChildren<MonsterController>();
        monsterController._characterGotIntoArea = true;
    }


    void OnGraivtyAndDisableCollider(Rigidbody rigidbody, Collider collider)
    {
        if (collider != null)
        {
            collider.enabled = false;
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
