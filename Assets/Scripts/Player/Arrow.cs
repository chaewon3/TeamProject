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

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        arrowcollider = GetComponent<CapsuleCollider>();
        arrowPool = FindObjectOfType<ArrowPool>();
        dmgPool = FindObjectOfType<DamageTextPool>();
    }

    void Start()
    {
        dmg = PlayerManager.Data.damage;
    }

    void OnEnable()
    {
        arrowcollider.enabled = true;
        Vector3 force = transform.up * speed;
        rigid.AddForce(force);
        StartCoroutine(Despawn(gameObject));
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
        if ((targetLayer | (1 << other.gameObject.layer)) != targetLayer)
        {
            return;
        }

        if (other.TryGetComponent<IHitable>(out IHitable hitable))
        {
            hitable.Hit(dmg);

            GameObject dmgText = dmgPool.GetObj(other.transform, dmg);
            StartCoroutine(Despawn(dmgText));
            print($"코루틴에 넣을 오브젝트 {dmgText}");
        }
        
        rigid.isKinematic = true;
        rigid.velocity = Vector3.zero;
        transform.SetParent(other.transform);
        arrowcollider.enabled = false;
    }

    IEnumerator Despawn(GameObject obj)
    {
        print($"코루틴 안에 있는 오브젝트 {obj}");
        yield return new WaitForSeconds(1.5f);
        print($"데미지 리턴");
        dmgPool.ReturnObj(obj);
        yield return new WaitForSeconds(1.5f);
        arrowPool.ReturnObj(gameObject);
    }

}