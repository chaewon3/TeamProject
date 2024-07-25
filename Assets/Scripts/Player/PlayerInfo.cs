using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour, IHitable
{
    #region 전역 변수
    PlayerMove player;
    Animator playerAni;
    PlayerSound sound;
    public float currentHealth;
    bool hit = false;
    bool isPlayerDead = false;

    public TextMeshProUGUI levelText;
    public Slider hpBar;
    public Slider expBar;

    public GameObject healParticle;
    public GameObject levelUpParticle;
    Coroutine healCoro, levelUpCoro;

    static readonly int isDead = Animator.StringToHash("isDead");
    static readonly int Dead = Animator.StringToHash("Dead"); 
    static readonly int HitBool = Animator.StringToHash("HitBool");
    static readonly int HitPlayer = Animator.StringToHash("Hit");
    #endregion

    void Awake()
    {
        player = FindObjectOfType<PlayerMove>();
        playerAni = GetComponent<Animator>();
        sound = GetComponent<PlayerSound>();
    }

    IEnumerator Start()
    {
        currentHealth = PlayerManager.Data.maxHealth;
        yield return new WaitForEndOfFrame();
        InventoryManager.Refresh();
    }

    void Update()
    {
        levelText.text = PlayerManager.Data.level.ToString();
        hpBar.maxValue = PlayerManager.Data.maxHealth;
        hpBar.value = currentHealth;
        expBar.maxValue = PlayerManager.Data.level * 100f; 
        expBar.value = PlayerManager.Data.experience;

        if (!isPlayerDead && currentHealth <= 0)
        {
            isPlayerDead = true;
            playerAni.SetBool(isDead, true);
            playerAni.SetTrigger(Dead);
            
            player.canMove = false;
            player.canRotat = false;

            SoundManager.Instance.VolumeDown();
            CanvasManager.Instance.PlayerDead();

        }

        if(PlayerManager.Data.experience >= PlayerManager.Data.level * 100f)
        {
            PlayerManager.Data.experience -= PlayerManager.Data.level * 100f;
            PlayerManager.Data.level++;
            levelUpParticle.SetActive(true);
            sound.LevelUpSound();
            if (levelUpCoro == null)
                levelUpCoro = StartCoroutine(OffLevelUp(levelUpParticle));

            if (PlayerManager.Data.level % 5 == 0)
            {
                PlayerManager.Data.skillPoint += 2;
            }
            else
                PlayerManager.Data.skillPoint++;
        }
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;
        if (hit)
            hit = false;
        else
            hit = true;

        playerAni.SetBool(HitBool, hit);
        playerAni.SetTrigger(HitPlayer);
    }
    public void Healing(float value)
    {
        currentHealth += value;
        healParticle.SetActive(true);
        if(healCoro == null)
            healCoro = StartCoroutine(OffHeal(healParticle));
    }

    IEnumerator OffHeal(GameObject ptc)
    {
        yield return new WaitForSeconds(.3f);
        ptc.SetActive(false);
    }

    IEnumerator OffLevelUp(GameObject ptc)
    {
        yield return new WaitForSeconds(1.6f);
        ptc.SetActive(false);
    }
}
