using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    float dmg;
    public LayerMask targetLayer;

    void OnCollisionEnter(Collision obj)
    {
        print("Ä®¿¡ ´ê¾Ò´Ù");
        if((targetLayer | (1 << obj.collider.gameObject.layer)) != targetLayer)
        {
            return;
        }

        if(obj.collider.TryGetComponent<IHitable>(out IHitable hitable))
        {
            hitable.Hit(dmg);
        }
    }
}
