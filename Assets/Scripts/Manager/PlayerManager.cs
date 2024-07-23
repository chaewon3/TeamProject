using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerData
{
    public float maxHealth = 100f;
    public float damage = 10f;
    public float experience = 0f;
    public int level = 1;
    public int ArrowCount = 12;
    public int skillPoint = 5;
    public int Gem = 0;
}

public class PlayerManager : MonoBehaviour, IHitable
{
    #region ���� ���� 
    public static PlayerManager Instance;

    PlayerData playerData = new PlayerData();
    PlayerMove player;
    Animator playerAni;

    float currentHealth;

    public TextMeshProUGUI levelText;
    public Slider hpBar;
    public Slider expBar;

    bool hit = false;

    [HideInInspector]
    public int ArrowCount;

    public static PlayerData Data { get => Instance.playerData;  set => Instance.playerData = value; }
    #endregion

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        player = FindObjectOfType<PlayerMove>();
        playerAni = FindObjectOfType<Animator>();

        currentHealth = playerData.maxHealth;
    }

    //void Update()
    //{
    //    // ������Ʈ���� �Լ��� ���� �ʿ��Ҷ��� ȣ�� �ϴ°� �����غ���
    //    levelText.text = playerData.level.ToString();
    //    hpBar.maxValue = playerData.maxHealth;
    //    hpBar.value = currentHealth;
    //    expBar.maxValue = playerData.level * 100f; // �̰� ���� ��� �����ϱ� 
    //    expBar.value = playerData.experience;
    //
    //    if(currentHealth <= 0)
    //    {
    //        player.canMove = false;
    //        playerAni.SetBool("isDead", true);
    //    }
    //}

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

    public void Healing(float value)
    {
        currentHealth += value;
        // ��ƼŬ?
    }
}


