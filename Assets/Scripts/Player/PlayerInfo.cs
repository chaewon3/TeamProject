using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour, IHitable
{
    // todo json에서 가져온 값으로 maxHp, dmg 초기화
    float maxHealth = 100f; 
    float currentHealth;
    float damage = 10; 

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;
    }
}

/*
 죽음 - MW@Death04

공격 - 1H-RH@Attack01 , 02, 03

피격 - 1H@TakeDamage02, 05
 */


