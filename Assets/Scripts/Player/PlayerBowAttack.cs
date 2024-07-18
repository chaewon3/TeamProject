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
    ArrowPooling pool;
    int arrow;
    int speed = 1200;

    public AnimationClip shootClip;
    public TextMeshProUGUI arrowText;
    #endregion

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        player = GetComponent<PlayerMove>();
        pool = GetComponent<ArrowPooling>();
        arrow = GetComponent<PlayerInfo>().ArrowCount;
    }

    void Start()
    {
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
                Shoot();

                if (ArrowCoroutine == null)
                    ArrowCoroutine = StartCoroutine(Arrow());
            }
        } 
    }

    void Shoot()
    {
        GameObject arrowObj = pool.GetObj();
        if(arrowObj != null)
        {
            if (arrowObj.TryGetComponent(out Rigidbody rigidBody))
                ApplyForce(rigidBody);
            StartCoroutine(Despawn(arrowObj));
            //또는 몬스터에 닿으면 set false
        }
    }

    void ApplyForce(Rigidbody rigid)
    {
        Vector3 force = transform.forward * speed;
        rigid.AddForce(force);
    }

    IEnumerator Arrow()
    {
        arrow--;
        arrowText.text = arrow.ToString();
        yield return new WaitForSeconds(shootClip.length);

        playerAnimator.SetBool("Charge", false);
        player.state = State.Sword;
        ArrowCoroutine = null;
    }

    IEnumerator Despawn(GameObject obj)
    {
        yield return new WaitForSeconds(3f);
        pool.ReturnObj(obj);
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