using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour, IHitable
{
    #region ���� ���� 
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
    // �÷��̾� ������ json���� ���̺� �Ҷ� ȭ���� playerBowAttack���� ���� �޾� �;��� 

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
        // ������Ʈ���� �Լ��� ���� �ʿ��Ҷ��� ȣ�� �ϴ°� �����غ���
        levelText.text = level.ToString();
        hpBar.maxValue = maxHealth;
        hpBar.value = currentHealth;
        expBar.maxValue = level * 100f; // �̰� ���� ��� �����ϱ� 
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


