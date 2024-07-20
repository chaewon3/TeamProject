using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour, IHitable
{
    #region ���� ���� 
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
    // �÷��̾� ������ json���� ���̺� �Ҷ� ȭ���� playerBowAttack���� ���� �޾� �;��� 

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
        // ������Ʈ���� �Լ��� ���� �ʿ��Ҷ��� ȣ�� �ϴ°� �����غ���
        levelText.text = level.ToString();
        hpBar.maxValue = maxHealth;
        hpBar.value = currentHealth;
        expBar.maxValue = level * 100f; // �̰� ���� ��� �����ϱ� 
        expBar.value = experience;
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;
    }
}


