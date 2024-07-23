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

    IEnumerator ComboAttack()
    {
        yield return new WaitForEndOfFrame();
        player.MoveLimit(false);
        playerAnimator.SetBool("CanAtk", false);
        //delray = false;
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
        yield return new WaitForSeconds(1.5f); //2F

        comboCount = 1;
        playerAnimator.SetInteger("Combo", comboCount);
        comboCoroutine = null;
    }

    public void OnCollider()
    {
        player.weapon[0].transform.GetChild(0).GetComponentInChildren<MeshCollider>().enabled = true;
        //if (player.weapon[0].activeSelf)
        //{ }
        //foreach (var item in player.weapon[0].gameObject)
        //{

        //}

    }

    public void OffCollider()
    {
        player.weapon[0].transform.GetChild(0).GetComponent<MeshCollider>().enabled = false;
    }

    public void HitPossible()
    {
        isHit = false;
    }
}

