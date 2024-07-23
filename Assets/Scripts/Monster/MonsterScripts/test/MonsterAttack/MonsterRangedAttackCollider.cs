using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRangedAttackCollider : MonoBehaviour
{
    int attackTimes;

    float damage;

    private void OnEnable()
    {
        StartCoroutine(DisappearTime());
        attackTimes = 0;
    }

    private void Update()
    {
        transform.position += transform.forward * 0.01f;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<IHitable>(out IHitable hitable))
        {
            attackTimes += 1;

            if (attackTimes == 1)
            {
                hitable.Hit(damage);
                this.gameObject.SetActive(false);
            }
        }
    }

    public void setDamage(float damage)
    {
        this.damage = damage;
    }

    IEnumerator DisappearTime()
    {
        yield return new WaitForSeconds(3.0f);

        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }
}
