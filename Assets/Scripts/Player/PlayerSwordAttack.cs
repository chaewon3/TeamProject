using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{
    Animator playerAnimator;
    bool comboing;
    int comboCount;
    public AnimationClip[] attakClip;
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
        comboCount = 0;
        comboing = false;
    }

    void Update()
    {
        if (player.canMove && player.state == State.Sword && Input.GetMouseButtonDown(0))
        {
            if (comboing)
            {
                //playerAnimator.SetBool("CanAtk", false);

                if (attackCoroutine != null)
                {
                    StopCoroutine(attackCoroutine);
                    playerAnimator.SetBool("CanAtk", false);
                    attackCoroutine = null;
                }
                    

                if (comboCoroutine != null && comboCount < 2)
                {
                    print("마지막 공격");
                    StopCoroutine(comboCoroutine);
                    playerAnimator.SetBool("CanAtk", false);
                    comboCoroutine = null;
                }

                if (comboCoroutine == null)
                {
                    comboCoroutine = StartCoroutine(AddCombo());
                }
                    
            }
            else
            {
                //comboing = true;
                //if (comboCoroutine == null)
                //    comboCoroutine = StartCoroutine(AddCombo());

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
        player.MoveChange(false);
        playerAnimator.SetBool("Combo", comboing);
        comboCount++;
        yield return new WaitForSeconds(attakClip[comboCount].length);
        //print($"comboCount: {comboCount}, attakClip.Length: {attakClip[comboCount].length}");

        comboing = false;
        comboCount = -1;
        playerAnimator.SetBool("Combo", comboing);
        print("아닛! 벌써 여기까지 도달했다고");

        yield return new WaitForEndOfFrame();
        playerAnimator.SetBool("CanAtk", true);

        player.MoveChange(true);
        comboCoroutine = null;
    }

    /// <summary>
    /// 첫 공격과 콤보 공격을 제어
    /// </summary>
    IEnumerator StartAttack()
    {
        comboing = true;
        yield return new WaitForEndOfFrame();

        playerAnimator.SetBool("CanAtk", false);
        yield return new WaitForSeconds(attakClip[0].length);

        print("다시 때릴 수 있어");
        playerAnimator.SetBool("CanAtk", true);
        comboing = false;
        attackCoroutine = null;
    }
}
// 이거 두번째 코루틴 필요없다 지우고 전부 콤보 코루틴으로 만들자



/*
 void Update()
    {
        if (player.state == State.Sword && Input.GetMouseButtonDown(0))
        {
            if(comboing)
            {
                if(comboCoroutine != null && comboCount < 2)
                {
                    StopCoroutine(comboCoroutine);
                    comboCoroutine = null;
                } 
            }
            else
            {
                if(attackCoroutine == null)
                    attackCoroutine = StartCoroutine(StartAttack());
            }

            if(comboCoroutine == null)//&& comboCount < 2
                comboCoroutine = StartCoroutine(AddCombo());

            playerAnimator.SetTrigger("Attack");
        }
    }

    /// <summary>
    /// 연속기 발동 코루틴 :
    /// 현재 공격 애니메이션이 끝나기 전에 공격 시 콤보 공격 발동
    /// </summary>
    IEnumerator AddCombo()
    {
        player.MoveChange(false);
        playerAnimator.SetBool("Combo", comboing);
        comboCount++;
        yield return new WaitForSeconds(attakClip[comboCount].length - 0.2f);

        comboing = false;
        comboCount = -1;
        playerAnimator.SetBool("Combo", comboing);
        yield return new WaitForSeconds(0.2f);
        playerAnimator.SetBool("CanAtk", true);

        player.MoveChange(true);
        comboCoroutine = null;
    }

    /// <summary>
    /// 첫 공격과 콤보 공격을 제어
    /// </summary>
    IEnumerator StartAttack()
    {
        comboing = true;
        yield return new WaitForEndOfFrame();
        playerAnimator.SetBool("CanAtk", false);
        print("중복실행");

        //yield return new WaitForSeconds(attakClip[0].length);
        attackCoroutine = null;
    }
==============================

void Update()
    {
        if (player.state == State.Sword && Input.GetMouseButtonDown(0))
        {
            if (comboing)
            {
                playerAnimator.SetBool("CanAtk", false);

                //if (attackCoroutine != null)
                //    StopCoroutine(attackCoroutine);

                if (comboCoroutine != null && comboCount < 2)
                {
                    StopCoroutine(comboCoroutine);
                    comboCoroutine = null;
                }

                if (comboCoroutine == null)//&& comboCount < 2
                    comboCoroutine = StartCoroutine(AddCombo());
            }
            else
            {
                if (attackCoroutine == null)
                    attackCoroutine = StartCoroutine(StartAttack());
            }

            

            playerAnimator.SetTrigger("Attack");
        }
    }
 */