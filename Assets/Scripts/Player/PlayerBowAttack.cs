using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerBowAttack : MonoBehaviour
{
    #region 전역 변수
    Animator playerAnimator;
    PlayerMove player;
    Coroutine ArrowCoroutine;
    ArrowPool pool;
    GameObject bow;
    int arrow;

    public AnimationClip shootClip;
    public TextMeshProUGUI arrowText;

    bool CanShoot = true;
    public Image coolTime;
    float fillAmount = 1f;
    float totalTime = 2f;
    #endregion

    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        player = GetComponent<PlayerMove>();
        pool = GetComponent<ArrowPool>();
        coolTime.fillAmount = 0f;
    }

    void Start()
    {
        arrow = PlayerManager.Data.ArrowCount;
        arrowText.text = arrow.ToString();
        FindBow();
    }

    void Update()
    {
        if (bow == null && Input.GetMouseButtonDown(1))
            FindBow();

        if (arrow != 0 && player.canMove && bow != null)
        {
            if(!bow.activeSelf)
                FindBow();

            if(bow != null && CanShoot)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (player.state != State.Bow)
                        player.state = State.Bow;

                    playerAnimator.SetTrigger("Charge");
                }

                if (Input.GetMouseButtonUp(1))
                {
                    CanShoot = false;
                    coolTime.fillAmount = 1f;

                    playerAnimator.SetTrigger("Attack");
                    pool.GetArrow();

                    if (ArrowCoroutine == null)
                        ArrowCoroutine = StartCoroutine(Arrow());
                }
            }
        }

        

        if (!CanShoot && fillAmount > 0)
        {
            fillAmount = fillAmount - (Time.deltaTime / (totalTime - 1));
            coolTime.fillAmount = fillAmount;

            if (coolTime.fillAmount == 0)
            {
                CanShoot = true;
                fillAmount = 1f;
            }
        }
    }

    void FindBow()
    {
        bow = null;

        for (int i = 0; i < player.weapon[1].transform.childCount; i++)
        {
            Transform childTransform = player.weapon[1].transform.GetChild(i);
            GameObject childObj = childTransform.gameObject;

            if (childObj.activeSelf)
            {
                bow = childObj;
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