using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    #region Àü¿ª º¯¼ö
    float dmg;
    public LayerMask targetLayer;
    DamageTextPool pool;
    //bool isHit = false;
    PlayerSwordAttack player;

    //bool hitEnemy = false;
    #endregion

    void Awake()
    {
        pool = FindObjectOfType<DamageTextPool>();
        player = FindObjectOfType<PlayerSwordAttack>();
    }

    void Start()
    {
        dmg = PlayerManager.Data.damage;
    }

    void OnCollisionEnter(Collision obj)
    {
        print("Ä®¿¡ ´ê¾Ò´Ù");
        if((targetLayer | (1 << obj.collider.gameObject.layer)) != targetLayer)
        {
            return;
        }

        if (!player.isHit && obj.collider.TryGetComponent<IHitable>(out IHitable hitable))
        {
            hitable.Hit(dmg);
            player.isHit = true;
            GameObject dmgText = pool.GetObj(obj.transform, dmg);
            StartCoroutine(Despawn(dmgText));
        }

        
    }

    IEnumerator Despawn(GameObject obj)
    {
        yield return new WaitForSeconds(3f);
        pool.ReturnObj(obj);
    }

    //public void HitPossible()
    //{
    //    isHit = false;
    //}
    
}
