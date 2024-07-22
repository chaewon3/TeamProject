using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    Collider ObjCollider;
    bool trigger = false;

    void Awake()
    {
        ObjCollider = GetComponent<Collider>();
    }
    public void OnCollider()
    {
        ObjCollider.enabled = true;
    }

    public void OffCollider()
    {
        ObjCollider.enabled = false;
    }
}
