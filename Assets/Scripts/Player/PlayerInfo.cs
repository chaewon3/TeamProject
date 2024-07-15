using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour, IHitable
{
    PlayerData playerData;
    // todo json���� ������ ������ maxHp, dmg �ʱ�ȭ
    float maxHealth; 
    float currentHealth;
    float damage;
    float experience;

    void Awake()
    {
        playerData = DataManager.Instance.playerData;
        maxHealth = playerData.maxHP;
        currentHealth = maxHealth;
        damage = playerData.damage;
        experience = playerData.experience;
        print($"PlayerDataSet => maxHP: {maxHealth}, damage: {damage}, experience: {experience}");
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


