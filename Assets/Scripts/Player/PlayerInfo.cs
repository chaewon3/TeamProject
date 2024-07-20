using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour, IHitable
{
    #region 전역 변수 
    PlayerData playerData;
    float maxHealth;
    float currentHealth;
    float damage;
    float experience;
    int level;

    public TextMeshProUGUI levelText;
    public Slider hpBar;
    public Slider expBar;

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
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;
    }
}


