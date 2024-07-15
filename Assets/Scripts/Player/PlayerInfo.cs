using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour, IHitable
{
    // todo json���� ������ ������ maxHp, dmg �ʱ�ȭ
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
 ���� - MW@Death04

���� - 1H-RH@Attack01 , 02, 03

�ǰ� - 1H@TakeDamage02, 05
 */


