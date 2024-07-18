using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrow : MonoBehaviour
{
    float dmg;
    public LayerMask targetLayer;
    Rigidbody rigid;
    CapsuleCollider collider;
    int speed = 1200;//1200
    ArrowPooling pool;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        pool = FindObjectOfType<ArrowPooling>();
    }

    void OnEnable()
    {
        collider.enabled = true;
        Vector3 force = transform.up * speed;
        rigid.AddForce(force);
        StartCoroutine(Despawn());
    }

    void OnDisable()
    {
        rigid.velocity = Vector3.zero;
        rigid.isKinematic = false;
    }

    //void OnTriggerEnter(Collider obj)
    //{
    //    print("명중");
    //    if ((targetLayer | (1 << obj.gameObject.layer)) != targetLayer)
    //    {
    //        print("너에게 줄건 아무것도 없다.");
    //        return;
    //    }

    //    if (obj.TryGetComponent<IHitable>(out IHitable hitable))
    //    {
    //        hitable.Hit(dmg);
    //    }

    //    rigid.isKinematic = true;
    //    rigid.velocity = Vector3.zero;
    //    transform.SetParent(obj.transform);
    //    collider.enabled = false;

    //}

    void OnCollisionEnter(Collision obj)
    {
        print("명중");
        if ((targetLayer | (1 << obj.collider.gameObject.layer)) != targetLayer)
        {
            print("너에게 줄건 아무것도 없다.");
            return;
        }

        if (obj.collider.TryGetComponent<IHitable>(out IHitable hitable))
        {
            hitable.Hit(dmg);
        }

        transform.SetParent(obj.transform);
        collider.enabled = false;
        rigid.isKinematic = true;
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(3f);
        pool.ReturnObj(gameObject);
    }

}

// 화살에 트리거 활성화하고 ray로 벽에 쏴서 판정으로 하기 싫어도 바꾸기 
/*
 void Update() {
    Ray ray = new Ray(transform.position, transform.forward);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, speed * Time.deltaTime)) {
        // 충돌 지점으로 화살 이동
        transform.position = hit.point;
        // 화살 멈추기
        rb.velocity = Vector3.zero;
    }
    else {
        // 화살 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
 */
