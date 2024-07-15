using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowAttack : MonoBehaviour
{
    Animator playerAnimator;
    PlayerMove player;

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

        if(Input.GetMouseButtonUp(1))
        {
            player.state = State.Sword;
        }
    }

    //void StateController(State state)
    //{
    //    switch(state)
    //    {
    //        case State.Sword:
    //            playerAnimator.SetBool("BowForm", false);
    //            playerAnimator.SetBool("SwordForm", true);  
    //            break;
    //        case State.Bow:
    //            playerAnimator.SetBool("SwordForm", false);
    //            playerAnimator.SetBool("BowForm", true);
    //            break;
    //    }
    //}
}
