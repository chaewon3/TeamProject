using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrow : MonoBehaviour
{
    float dmg;
    public LayerMask targetLayer;
    Rigidbody rigid;
    CapsuleCollider arrowcollider;
    int speed = 1200;//1200
    ArrowPooling pool;

    bool end = false;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        arrowcollider = GetComponent<CapsuleCollider>();
        pool = FindObjectOfType<ArrowPooling>();
    }

    void OnEnable()
    {
        arrowcollider.enabled = true;
        Vector3 force = transform.up * speed;
        rigid.AddForce(force);
        StartCoroutine(Despawn());
    }

    void OnDisable()
    {
        rigid.velocity = Vector3.zero;
        rigid.isKinematic = false;
    }

    void FixedUpdate()
    {
        end = Physics.Raycast(transform.position, transform.up, 0.3f, targetLayer);

        if (end)
        {
            rigid.velocity = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider obj)
    {
        if ((targetLayer | (1 << obj.gameObject.layer)) != targetLayer)
        {
            return;
        }

        if (obj.TryGetComponent<IHitable>(out IHitable hitable))
        {
            hitable.Hit(dmg);
        }

        rigid.isKinematic = true;
        rigid.velocity = Vector3.zero;
        transform.SetParent(obj.transform);
        arrowcollider.enabled = false;
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(3f);
        pool.ReturnObj(gameObject);
    }

}