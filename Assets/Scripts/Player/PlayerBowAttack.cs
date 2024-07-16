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
        if(Input.GetMouseButtonDown(1))
        {
            if (player.state != State.Bow)
                player.state = State.Bow;

            playerAnimator.SetTrigger("Charge");
        }           

        if (Input.GetMouseButtonUp(1))
        {
            playerAnimator.SetTrigger("Attack");

            if(arrow == null)
                arrow = StartCoroutine(Arrow());
        }
    }

    IEnumerator Arrow()
    {

        yield return new WaitForSeconds(shootClip.length);

        playerAnimator.SetBool("Charge", false);
        player.state = State.Sword;
        arrow = null;
    }
}

/*
 if(Input.GetMouseButton(1))
        {
            if(player.state != State.Bow)
                player.state = State.Bow;
        }

        if(Input.GetMouseButtonDown(1))
            playerAnimator.SetTrigger("Charge");
 */