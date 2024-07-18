using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour, IHitable
{
    #region 전역 변수 
    PlayerData playerData;
    float maxHealth;
    float currentHealth;
    float damage;
    float experience;
    int level;

    [HideInInspector]
    public int ArrowCount;
    #endregion
    // 플레이어 정보를 json으로 세이브 할때 화살은 playerBowAttack에서 따로 받아 와야함 

    void Awake()
    {
        playerData = DataManager.Instance.playerData;
        maxHealth = playerData.maxHP;
        currentHealth = maxHealth;
        damage = playerData.damage;
        experience = playerData.experience;
        level = playerData.level;
        print($"PlayerDataSet => maxHP: {maxHealth}, damage: {damage}, experience: {experience}, level: {level}");

        ArrowCount = playerData.ArrowCount;
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;
    }
}

/*
 죽음 - MW@Death04

피격 - 1H@TakeDamage02, 05
 */


