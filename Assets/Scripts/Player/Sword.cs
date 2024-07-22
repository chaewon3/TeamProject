using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    #region Àü¿ª º¯¼ö
    float dmg;
    public LayerMask targetLayer;
    DamageTextPool pool;

    //Collider ObjCollider;
    //bool hitEnemy = false;
    #endregion

    void Awake()
    {
        //dmg = PlayerInfo.damage; 
        pool = FindObjectOfType<DamageTextPool>();
        //ObjCollider = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision obj)
    {
        print("Ä®¿¡ ´ê¾Ò´Ù");
        if((targetLayer | (1 << obj.collider.gameObject.layer)) != targetLayer)
        {
            return;
        }

        if ( obj.collider.TryGetComponent<IHitable>(out IHitable hitable))
        {
            hitable.Hit(dmg);

            GameObject dmgText = pool.GetObj(obj.transform, dmg);
            StartCoroutine(Despawn(dmgText));
        }

        
    }

    IEnumerator Despawn(GameObject obj)
    {
        yield return new WaitForSeconds(3f);
        pool.ReturnObj(obj);
    }

    
}
