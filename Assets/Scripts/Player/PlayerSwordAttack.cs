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
    }

    void Update()
    {
        if(player.state == State.Sword && Input.GetMouseButtonDown(0))
        {
            if (delray)
            {
                playerAnimator.SetTrigger("Attack");

                if (comboCoroutine != null && comboCount != 4)
                {
                    StopCoroutine(comboCoroutine);
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

    IEnumerator ComboAttack()
    {
        yield return new WaitForEndOfFrame();
        playerAnimator.SetBool("CanAtk", false);
        //delray = false;
        comboCount++;
        playerAnimator.SetInteger("Combo", comboCount);
        yield return new WaitForSeconds(attackClip[comboCount-2].length / 1.5f);

        delray = true;
        playerAnimator.SetBool("CanAtk", true);
        if (comboCount == 4)
        {
            comboCount = 1;
            playerAnimator.SetInteger("Combo", comboCount);
            StopCoroutine(comboCoroutine);
            comboCoroutine = null;
        }
        yield return new WaitForSeconds(1.5f); //2F

        comboCount = 1;
        playerAnimator.SetInteger("Combo", comboCount);
        comboCoroutine = null;
    }
}

