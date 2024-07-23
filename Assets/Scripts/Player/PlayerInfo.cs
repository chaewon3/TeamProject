using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerInfo : MonoBehaviour, IHitable
{
    PlayerMove player;
    Animator playerAni;
    float currentHealth;
    bool hit = false;

    public TextMeshProUGUI levelText;
    public Slider hpBar;
    public Slider expBar;

    void Awake()
    {
        player = FindObjectOfType<PlayerMove>();
        playerAni = FindObjectOfType<Animator>();
    }

    void Start()
    {
        currentHealth = PlayerManager.Data.maxHealth;
    }

    void Update()
    {
        // ������Ʈ���� �Լ��� ���� �ʿ��Ҷ��� ȣ�� �ϴ°� �����غ���
        levelText.text = PlayerManager.Data.level.ToString();
        hpBar.maxValue = PlayerManager.Data.maxHealth;
        hpBar.value = currentHealth;
        expBar.maxValue = PlayerManager.Data.level * 100f; // �̰� ���� ��� �����ϱ� 
        expBar.value = PlayerManager.Data.experience;

        if (currentHealth <= 0)
        {
            player.canMove = false;
            playerAni.SetBool("isDead", true);
        }
    }

    public void Hit(float damage)
    {
        print($"�������� {damage}������ ���� hp : {currentHealth}");
        currentHealth -= damage;
        if (hit)
            hit = false;
        else
            hit = true;

        playerAni.SetBool("HitBool", hit);
        playerAni.SetTrigger("Hit");
    }
    public void Healing(float value)
    {
        currentHealth += value;
        // ��ƼŬ?
    }
}
