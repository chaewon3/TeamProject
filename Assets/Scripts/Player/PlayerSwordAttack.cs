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
                //playerAnimator.SetInteger("Combo", comboCount);
                playerAnimator.SetTrigger("Attack");

                if (comboCoroutine != null && comboCount != 3)
                { 
                    StopCoroutine(comboCoroutine);
                    comboCoroutine = null;
                }

                if (comboCoroutine == null)
                    comboCoroutine = StartCoroutine(ComboAttack());
            }
                
        }
    }

    IEnumerator ComboAttack()
    {
        yield return new WaitForEndOfFrame();
        playerAnimator.SetBool("CanAtk", false);
        delray = false;
        comboCount++;
        playerAnimator.SetInteger("Combo", comboCount);
        yield return new WaitForSeconds(attackClip[comboCount-1].length / 1.5f);

        delray = true;
        playerAnimator.SetBool("CanAtk", true);
        yield return new WaitForSeconds(2f);
        // 이게 콤보 3단계에서 터지면 안 됨 그것만 해결하면 끝

        comboCount = 1;
        playerAnimator.SetInteger("Combo", comboCount);
        comboCoroutine = null;
    }
}


/*
 void Update()
    {
        if (player.state == State.Sword && Input.GetMouseButtonDown(0))
        {
            if (comboing)
            {
                if (attackCoroutine != null)
                {
                    StopCoroutine(attackCoroutine);
                    attackCoroutine = null;
                }   

                if (comboCoroutine != null && comboCount < 2)
                {
                    StopCoroutine(comboCoroutine);
                    comboCoroutine = null;
                }

                if (comboCoroutine == null && comboCount < 2)
                {
                    comboCoroutine = StartCoroutine(AddCombo());
                    print("check");
                } 
            }
            else
            {
                if (attackCoroutine == null)
                    attackCoroutine = StartCoroutine(StartAttack());
            }

            playerAnimator.SetTrigger("Attack");
        }
    }

    /// <summary>
    /// 연속기 발동 코루틴 :
    /// 현재 공격 애니메이션이 끝나기 전에 공격 시 콤보 공격 발동
    /// </summary>
    IEnumerator AddCombo()
    {
        //player.canMove = false;
        playerAnimator.SetBool("Combo", comboing);
        comboCount++;
        yield return new WaitForSeconds(attackClip[comboCount].length );

        comboing = false;
        playerAnimator.SetBool("Combo", comboing);
        comboCount = 0;

        //yield return new WaitForSeconds(0.5f);
        yield return new WaitForEndOfFrame();
        playerAnimator.SetBool("CanAtk", true);

        player.canMove = true;
        comboCoroutine = null;
    }

    /// <summary>
    /// 첫 공격과 콤보 공격을 제어
    /// </summary>
    IEnumerator StartAttack()
    {
        player.canMove = false;
        comboing = true;
        yield return new WaitForEndOfFrame();

        playerAnimator.SetBool("CanAtk", false);
        yield return new WaitForSeconds(attackClip[0].length);

        player.canMove = true;
        playerAnimator.SetBool("CanAtk", true);
        comboing = false;
        attackCoroutine = null;
    }
 */