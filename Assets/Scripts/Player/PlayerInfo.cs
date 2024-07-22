using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour, IHitable
{
    #region 전역 변수 
    PlayerData playerData;
    PlayerMove player;
    Animator playerAni;
    float maxHealth;
    float currentHealth;
    float damage;
    float experience;
    int level;

    public TextMeshProUGUI levelText;
    public Slider hpBar;
    public Slider expBar;

    bool hit = false;

    [HideInInspector]
    public int ArrowCount;
    #endregion
    // 플레이어 정보를 json으로 세이브 할때 화살은 playerBowAttack에서 따로 받아 와야함 

    void Awake()
    {
        playerData = DataManager.Instance.playerDataTest;
        player = GetComponent<PlayerMove>();
        playerAni = GetComponent<Animator>();
        maxHealth = playerData.maxHP;
        currentHealth = maxHealth;
        damage = playerData.damage;
        experience = playerData.experience;
        level = playerData.level;
        //print($"PlayerDataSet => maxHP: {maxHealth}, damage: {damage}, experience: {experience}, level: {level}");

        ArrowCount = playerData.ArrowCount;
    }

    void Update()
    {
        // 업데이트말고 함수로 빼서 필요할때만 호출 하는거 생각해보기
        levelText.text = level.ToString();
        hpBar.maxValue = maxHealth;
        hpBar.value = currentHealth;
        expBar.maxValue = level * 100f; // 이거 비율 계산 조율하기 
        expBar.value = experience;

        if(currentHealth <= 0)
        {
            player.canMove = false;
            playerAni.SetBool("isDead", true);
        }
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;

        if (hit)
            hit = false;
        else
            hit = true;

        playerAni.SetBool("HitBool", hit);
        playerAni.SetTrigger("Hit");
    }
}


