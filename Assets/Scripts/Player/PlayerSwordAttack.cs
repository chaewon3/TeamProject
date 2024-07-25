using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{
    Animator playerAnimator;
    bool comboing;
    bool delray = true;
    int comboCount;
    public AnimationClip[] attackClip;
    Coroutine attackCoroutine, comboCoroutine;
    PlayerMove player;

    GameObject sword;
    GameObject particle;

    public bool isHit = false;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        player = GetComponent<PlayerMove>();
    }

    void Start()
    {
        playerAnimator.SetBool("CanAtk", true);
        comboCount = 1;
        playerAnimator.SetInteger("Combo", comboCount);
        comboing = false;
        FindSword();
    }

    void Update()
    {
        if(sword != null && Input.GetMouseButtonDown(0))
        {
            if(!sword.activeSelf)
                FindSword();

            if (player.state == State.Sword && player.canMove && sword != null)
            {
                if (delray)
                {
                    playerAnimator.SetTrigger("Attack");

                    if (comboCoroutine != null && comboCount != 4)
                    {
                        StopCoroutine(comboCoroutine);
                        player.MoveLimit(true);
                        comboCoroutine = null;
                    }

                    if (comboCoroutine == null)
                    {
                        delray = false;
                        comboCoroutine = StartCoroutine(ComboAttack());
                    }
                }
            }
        }

        if(sword == null && Input.GetMouseButtonDown(0))
            FindSword();
    }

    IEnumerator ComboAttack()
    {
        yield return new WaitForEndOfFrame();
        player.MoveLimit(false);
        playerAnimator.SetBool("CanAtk", false);
        comboCount++;
        playerAnimator.SetInteger("Combo", comboCount);
        yield return new WaitForSeconds(attackClip[comboCount-2].length / 1.5f);

        player.MoveLimit(true);
        delray = true;
        playerAnimator.SetBool("CanAtk", true);
        if (comboCount == 4)
        {
            comboCount = 1;
            playerAnimator.SetInteger("Combo", comboCount);
            StopCoroutine(comboCoroutine);
            comboCoroutine = null;
        }
        yield return new WaitForSeconds(1.5f); 

        comboCount = 1;
        playerAnimator.SetInteger("Combo", comboCount);
        comboCoroutine = null;
    }

    void FindSword()
    {
        sword = null;

        for (int i = 0; i < player.weapon[0].transform.childCount; i++)
        {
            Transform childTransform = player.weapon[0].transform.GetChild(i);
            GameObject childObj = childTransform.gameObject;

            if (childObj.activeInHierarchy)
            {
                sword = childObj;
                particle = sword.transform.GetChild(0).gameObject;
            }
                
        }
    }

    public void OnCollider()
    {
        if(sword.activeSelf)
            sword.GetComponent<MeshCollider>().enabled = true;
        else
        {
            FindSword();
            sword.GetComponent<MeshCollider>().enabled = true;
        }
    }

    public void OffCollider()
    {
        if (sword.activeSelf)
            sword.GetComponent<MeshCollider>().enabled = false;
        else
        {
            FindSword();
            sword.GetComponent<MeshCollider>().enabled = false;
        }
    }

    public void HitPossible()
    {
        isHit = false;
    }

    public void OnParticle()
    {
        if (sword.activeSelf)
            particle.SetActive(true);
        else
        {
            FindSword();
            particle.SetActive(true);
        }
    }

    public void OffParticle()
    {
        if (sword.activeSelf)
            particle.SetActive(false);
        else
        {
            FindSword();
            particle.SetActive(false);
        }
    }
}

