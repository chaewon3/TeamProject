using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRaycast : MonoBehaviour
{
    int attackTimes;

    float damage;

    public LayerMask groundLayer;
    Vector3 rayOrigin;
    Vector3 rayDirection;
    private void Start()
    {
        damage = 30;
    }


    private void OnEnable()
    {
        StartCoroutine(RaycastStart());

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

        gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.TryGetComponent<IHitable>(out IHitable hitable))
        {
            hitable.Hit(damage);

            gameObject.SetActive(false);
        }
    }

    //public void SetDamage(float damage)
    //{
    //    this.damage = damage;
    //}
}
