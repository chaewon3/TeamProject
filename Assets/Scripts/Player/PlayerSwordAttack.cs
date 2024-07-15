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
    /// ���ӱ� �ߵ� �ڷ�ƾ :
    /// ���� ���� �ִϸ��̼��� ������ ���� ���� �� �޺� ���� �ߵ�
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
    /// ù ���ݰ� �޺� ������ ����
    /// </summary>
    IEnumerator StartAttack()
    {
        playerAnimator.SetTrigger("Attack");
        yield return new WaitForEndOfFrame();
        playerAnimator.SetBool("CanAtk", false);
    }
}
