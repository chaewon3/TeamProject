using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour, IHitable
{
    PlayerMove player;
    Animator playerAni;
    public float currentHealth;
    bool hit = false;
    bool isDead = false;

    public TextMeshProUGUI levelText;
    public Slider hpBar;
    public Slider expBar;

    public GameObject healParticle;
    public GameObject levelUpParticle;
    Coroutine healCoro, levelUpCoro;

    void Awake()
    {
        player = FindObjectOfType<PlayerMove>();
        playerAni = GetComponent<Animator>();
    }

    IEnumerator Start()
    {
        currentHealth = PlayerManager.Data.maxHealth;
        yield return new WaitForEndOfFrame();
        InventoryManager.Refresh();
    }

    void Update()
    {
        // 업데이트말고 함수로 빼서 필요할때만 호출 하는거 생각해보기
        levelText.text = PlayerManager.Data.level.ToString();
        hpBar.maxValue = PlayerManager.Data.maxHealth;
        hpBar.value = currentHealth;
        expBar.maxValue = PlayerManager.Data.level * 100f; // 이거 비율 계산 조율하기 
        expBar.value = PlayerManager.Data.experience;

        if (!isDead && currentHealth <= 0)
        {
            isDead = true;
            playerAni.SetBool("isDead", true);
            playerAni.SetTrigger("Dead");
            
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
            if(levelUpCoro == null)
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

        playerAni.SetBool("HitBool", hit);
        playerAni.SetTrigger("Hit");
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
