using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour, IHitable
{
    PlayerData playerData;
    // todo json에서 가져온 값으로 maxHp, dmg 초기화
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
 죽음 - MW@Death04

공격 - 1H-RH@Attack01 , 02, 03

피격 - 1H@TakeDamage02, 05
 */


