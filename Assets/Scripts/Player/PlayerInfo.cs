using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour, IHitable
{
    PlayerData playerData;
    float maxHealth; 
    float currentHealth;
    float damage;
    float experience;
    int level;
    List<PlayerData> asd = new List<PlayerData>(); 

    void Awake()
    {
        playerData = DataManager.Instance.playerData;
        maxHealth = playerData.maxHP;
        currentHealth = maxHealth;
        damage = playerData.damage;
        experience = playerData.experience;
        level = playerData.level;
        print($"PlayerDataSet => maxHP: {maxHealth}, damage: {damage}, experience: {experience}, level: {level}");
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;
    }
}

/*
 ���� - MW@Death04

�ǰ� - 1H@TakeDamage02, 05
 */


