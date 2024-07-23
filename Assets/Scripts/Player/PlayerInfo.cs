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
        // 업데이트말고 함수로 빼서 필요할때만 호출 하는거 생각해보기
        levelText.text = PlayerManager.Data.level.ToString();
        hpBar.maxValue = PlayerManager.Data.maxHealth;
        hpBar.value = currentHealth;
        expBar.maxValue = PlayerManager.Data.level * 100f; // 이거 비율 계산 조율하기 
        expBar.value = PlayerManager.Data.experience;

        if (currentHealth <= 0)
        {
            player.canMove = false;
            playerAni.SetBool("isDead", true);
        }
    }

    public void Hit(float damage)
    {
        print($"보스한테 {damage}데미지 남은 hp : {currentHealth}");
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
        // 파티클?
    }
}
