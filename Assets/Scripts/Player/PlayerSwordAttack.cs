using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{
    Animator playerAnimator;
    bool comboing = false;
    public AnimationClip[] attakClip;
    Coroutine coroutine;
    PlayerMove player;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        player = GetComponent<PlayerMove>();
    }

    void Start()
    {
        playerAnimator.SetBool("SwordForm", true);
        playerAnimator.SetBool("CanAtk", true);
    }

    void Update()
    {
        if (player.state == State.Sword && Input.GetMouseButtonDown(0))
        {
            if(comboing)
            {
                StopCoroutine(coroutine);     
            }
            else
            {
                StartCoroutine(StartAttack());
                comboing = true;
            }

            coroutine = StartCoroutine(AddCombo());
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
        yield return new WaitForSeconds(attakClip[1].length);

        comboing = false;
        playerAnimator.SetBool("Combo", comboing);
        playerAnimator.SetBool("CanAtk", true);
        yield return new WaitForSeconds(0.7f);

        player.MoveChange(true);
    }

    /// <summary>
    /// 첫 공격과 콤보 공격을 제어
    /// </summary>
    IEnumerator StartAttack()
    {
        playerAnimator.SetTrigger("Attack");
        yield return new WaitForEndOfFrame();
        playerAnimator.SetBool("CanAtk", false);
    }
}
