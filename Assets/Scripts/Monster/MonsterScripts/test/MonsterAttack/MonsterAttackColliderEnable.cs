using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackColliderEnable : MonoBehaviour
{
    int attackTimes;

    float damage;

    private void Start()
    {
        damage = GetComponentInParent<MonsterController>().monsterInfo._attackDamage;
    }

    private void OnEnable()
    {
        attackTimes = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<IHitable>(out IHitable hitable))
        {
            attackTimes += 1;
            
            if (attackTimes == 1)
            {
                hitable.Hit(damage);
            }
        }
    }
}
