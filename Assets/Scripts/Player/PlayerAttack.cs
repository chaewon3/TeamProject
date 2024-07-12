using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator playerAnimator;
    bool comboing = false;
    public AnimationClip[] attakClip;
    Coroutine coroutine;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();    
    }

    void Start()
    {
        playerAnimator.SetBool("SwordForm", true);
        playerAnimator.SetBool("CanAtk", true);

        //print(attakClip[0].length);
        //print(attakClip[1].length);
        //print(attakClip[2].length);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("근접 공격");

            if(comboing)
            {
                print("코루틴 정지 ");
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

    IEnumerator AddCombo()
    {
        print("콤보 시작");
        playerAnimator.SetBool("Combo", comboing);
        yield return new WaitForSeconds(attakClip[1].length);

        print("콤보 끝");
        comboing = false;
        playerAnimator.SetBool("Combo", comboing);
        playerAnimator.SetBool("CanAtk", true);
        
    }

    IEnumerator StartAttack()
    {
        playerAnimator.SetTrigger("Attack");
        yield return new WaitForEndOfFrame();
        playerAnimator.SetBool("CanAtk", false);
    }
}
