using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float dmg;
    public LayerMask targetLayer;
    Rigidbody rigid;
    CapsuleCollider arrowcollider;
    int speed = 1200;
    ArrowPool arrowPool;
    DamageTextPool dmgPool;
    bool end = false;
    Coroutine despawn;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        arrowPool = FindObjectOfType<ArrowPool>();
        arrowcollider = GetComponent<CapsuleCollider>();
        dmgPool = FindObjectOfType<DamageTextPool>();
    }

    void Start()
    {
        dmg = PlayerManager.Data.longDamage;
    }

    void OnEnable()
    {
        arrowcollider.enabled = true;
        Vector3 force = transform.up * speed;
        rigid.AddForce(force);
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

    void OnTriggerEnter(Collider other)
    {
        if (despawn == null)
            despawn = StartCoroutine(DespawnArrow());

        if ((targetLayer | (1 << other.gameObject.layer)) != targetLayer)
        {
            return;
        }

        if (other.TryGetComponent<IHitable>(out IHitable hitable))
        {
            hitable.Hit(dmg);

            GameObject dmgText = dmgPool.GetDmgText(other.transform, dmg);
            StartCoroutine(Despawn(dmgText));
        }
        
        rigid.isKinematic = true;
        rigid.velocity = Vector3.zero;
        transform.SetParent(other.transform);
        arrowcollider.enabled = false;
    }

    IEnumerator Despawn(GameObject obj)
    {
        yield return new WaitForSeconds(1.5f);
        dmgPool.ReturnDmgText(obj);
    }

    IEnumerator DespawnArrow()
    {
        yield return new WaitForSeconds(3f);
        arrowPool.ReturnArrow(gameObject);
        despawn = null;
    }

}