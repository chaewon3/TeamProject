using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBowAttack : MonoBehaviour
{
    #region 전역 변수
    Animator playerAnimator;
    PlayerMove player;
    Coroutine ArrowCoroutine;
    ArrowPool pool;
    int arrow;

    public AnimationClip shootClip;
    public TextMeshProUGUI arrowText;
    #endregion

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        player = GetComponent<PlayerMove>();
        pool = GetComponent<ArrowPool>();
    }

    void Start()
    {
        arrow = PlayerManager.Data.ArrowCount;
        arrowText.text = arrow.ToString();
    }

    void Update()
    {
        if(arrow != 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (player.state != State.Bow)
                    player.state = State.Bow;

                playerAnimator.SetTrigger("Charge");
            }

            if (Input.GetMouseButtonUp(1))
            {
                playerAnimator.SetTrigger("Attack");
                pool.GetObj();

                if (ArrowCoroutine == null)
                    ArrowCoroutine = StartCoroutine(Arrow());
            }
        } 
    }

    IEnumerator Arrow()
    {
        arrow--;
        PlayerManager.Data.ArrowCount--;
        arrowText.text = arrow.ToString();
        yield return new WaitForSeconds(shootClip.length);

        playerAnimator.SetBool("Charge", false);
        player.state = State.Sword;
        ArrowCoroutine = null;
    }
}