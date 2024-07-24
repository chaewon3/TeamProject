using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRaycast : MonoBehaviour
{
    int attackTimes;

    float damage;

    public GameObject smokePrefab;

    public LayerMask groundLayer;
    Vector3 rayOrigin;
    Vector3 rayDirection;
    private void Start()
    {
        damage = 30;
        
    }


    private void OnEnable()
    {
        attackTimes = 0;
        Initializing();

        StartCoroutine(RaycastStart());
        smokePrefab.SetActive(false);

    }

    IEnumerator RaycastStart()
    {
        gameObject.GetComponent<Collider>().enabled = true;

        while (true)
        {
            rayOrigin = transform.position;
            rayDirection = Vector3.down;

            Vector3 downwardMovement = new Vector3(0, -9.7f, 0);

            transform.position += downwardMovement * Time.deltaTime;


            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, 0.25f, groundLayer))
            {
                smokePrefab.SetActive(true);

                break;
            }

            yield return new WaitForSeconds(0.01f);
        }

        StartCoroutine(DisappearPillar());
    }

    IEnumerator DisappearPillar()
    {
        int time = 0;

        gameObject.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(2.5f);

        while (true)
        {
            time += 1;
            Vector3 downwardMovement = new Vector3(0, -0.03f, 0);

            transform.position += downwardMovement;
            yield return new WaitForSeconds(0.01f);

            if (time >= 200)
            {
                break;
            }
        }

        this.transform.parent.gameObject.SetActive(false);
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.TryGetComponent<IHitable>(out IHitable hitable))
        {
            attackTimes += 1;

            if (attackTimes == 1)
            {
                print(11);
                smokePrefab.SetActive(true);
                hitable.Hit(damage);

            }
        }
    }

    void Initializing()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.transform.localPosition = Vector3.zero;

    }

}
