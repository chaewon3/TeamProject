using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowAttack : MonoBehaviour
{
    Animator playerAnimator;
    PlayerMove player;
    public AnimationClip shootClip;
    Coroutine arrow;

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        player = GetComponent<PlayerMove>();
    }

    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            print("누르고 있는중");
            player.state = State.Bow;
            
        }

        if(Input.GetMouseButtonDown(1))
            playerAnimator.SetTrigger("Charge");

        if (Input.GetMouseButtonUp(1))
        {
            playerAnimator.SetTrigger("Attack");

            if(arrow == null)
                arrow = StartCoroutine(Arrow());
        }
    }

    IEnumerator Arrow()
    {
        print("arrow 코루틴 들어옴");
        //playerAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(shootClip.length);

        playerAnimator.SetBool("Charge", false);
        player.state = State.Sword;
        arrow = null;
    }
}
