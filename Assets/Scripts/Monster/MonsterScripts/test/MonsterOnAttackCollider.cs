using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOnAttackCollider : MonoBehaviour
{
    public GameObject armCollider;

    void BossMonsterOnAttack()
    {
        armCollider.SetActive(true);
    }

    void BossMonsterAfterAttack()
    {
        armCollider.SetActive(false);
    }

}
