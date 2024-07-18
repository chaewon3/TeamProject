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
    //    print("����");
    //    if ((targetLayer | (1 << obj.gameObject.layer)) != targetLayer)
    //    {
    //        print("�ʿ��� �ٰ� �ƹ��͵� ����.");
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
        print("����");
        if ((targetLayer | (1 << obj.collider.gameObject.layer)) != targetLayer)
        {
            print("�ʿ��� �ٰ� �ƹ��͵� ����.");
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

// ȭ�쿡 Ʈ���� Ȱ��ȭ�ϰ� ray�� ���� ���� �������� �ϱ� �Ⱦ �ٲٱ� 
/*
 void Update() {
    Ray ray = new Ray(transform.position, transform.forward);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, speed * Time.deltaTime)) {
        // �浹 �������� ȭ�� �̵�
        transform.position = hit.point;
        // ȭ�� ���߱�
        rb.velocity = Vector3.zero;
    }
    else {
        // ȭ�� �̵�
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
 */
