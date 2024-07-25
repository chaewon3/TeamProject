using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    #region ���� ����
    float dmg;
    public LayerMask targetLayer;

    DamageTextPool pool;
    PlayerSwordAttack player;
    PlayerSound playerSound;
    #endregion

    void Awake()
    {
        pool = FindObjectOfType<DamageTextPool>();
        player = FindObjectOfType<PlayerSwordAttack>();
        playerSound = FindObjectOfType<PlayerSound>();
    }

    void Start()
    {
        dmg = PlayerManager.Data.meleeDamage;
    }

    void OnEnable()
    {
        dmg = PlayerManager.Data.meleeDamage;
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
            player.isHit = true;
            playerSound.SwordHitEnemy();

            GameObject dmgText = pool.GetDmgText(other.transform, dmg);
            StartCoroutine(Despawn(dmgText));
        }
    }

    IEnumerator Despawn(GameObject obj)
    {
        yield return new WaitForSeconds(1.5f);
        pool.ReturnDmgText(obj);
    }
}
