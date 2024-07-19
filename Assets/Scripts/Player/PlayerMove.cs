using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region 전역 변수
    public float moveSpeed = 3f;
    CharacterController charCont;
    Animator playerAnimator;
    public bool canMove = false;

    public float dirSpeed; // 나중에 지울거 

    float gravity = -3f;
    bool isGrounded;
    LayerMask groundMask;
    Transform transGroundCheckPoint;

    float mouseSensitivity = 200f;
    float dirX;
    float dirY;
    Coroutine dushCoroutine;

    [HideInInspector]
    public State state;
    public GameObject[] weapon;
    #endregion


    void Awake()
    {
        charCont = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();

        transGroundCheckPoint = transform;
        groundMask = (1 << LayerMask.NameToLayer("Ground"));
    }

    void Start()
    {
        state = State.Sword;
    }

    void Update()
    {
        if(canMove)
        {
            #region 이동
         
            Vector3 moveDir = Vector3.zero;
            moveDir.z = Input.GetAxisRaw("Vertical");
            moveDir.x = Input.GetAxisRaw("Horizontal");

            charCont.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
            charCont.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);

            playerAnimator.SetFloat("Xposition", moveDir.x);
            playerAnimator.SetFloat("Yposition", moveDir.z);
            playerAnimator.SetFloat("Speed", moveDir.magnitude);
            #endregion

            #region 중력 

            isGrounded = Physics.Raycast(transGroundCheckPoint.position, Vector3.down, 0.2f, groundMask);

            if (!isGrounded)
            {
                charCont.Move(transform.up * gravity * Time.deltaTime);
            }
            #endregion

            #region 마우스 방향 회전

            dirX += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
            dirY -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
            
            dirY = Mathf.Clamp(dirY, -90f, 90f);
            transform.localRotation = Quaternion.Euler(0, dirX * dirSpeed, 0f);
            #endregion

            #region 대시
            if (Input.GetKeyDown(KeyCode.Space))// isground 조건 추가?
            {
                print("구릅니닷");
                playerAnimator.SetTrigger("Dush"); 

                if(dushCoroutine == null)
                    dushCoroutine = StartCoroutine(Dush());
            }
            #endregion //누른 방향키에 따라 캐릭을 회전? 다른 애니메이션 찾기?
        }

        StateController(state); //이거 계속 호출하는거 뭔가 이상함 낭비 같은?
    }

    IEnumerator Dush()
    {
        moveSpeed = 6;
        charCont.height = 1f;
        charCont.center = new Vector3(0, 0.54f, 0);
        yield return new WaitForSeconds(0.8f); //AniClip.Length = 0.833f

        moveSpeed = 3;
        charCont.height = 1.88f;
        charCont.center = new Vector3(0, 0.93f, 0);
        dushCoroutine = null;
    }

    public void MoveChange(bool bValue)
    {
        if(!bValue)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }

    void StateController(State state)
    {
        if(canMove)
        {
            switch (state)
            {
                case State.Sword:
                    playerAnimator.SetBool("BowForm", false);
                    weapon[1].SetActive(false);
                    playerAnimator.SetBool("SwordForm", true);
                    weapon[0].SetActive(true);
                    playerAnimator.SetLayerWeight(1, 0);
                    break;
                case State.Bow:
                    playerAnimator.SetBool("SwordForm", false);
                    weapon[0].SetActive(false);
                    playerAnimator.SetBool("BowForm", true);
                    weapon[1].SetActive(true);
                    playerAnimator.SetLayerWeight(1, 1);
                    break;
            }
        } 
    }
}

public enum State
{
    Sword,
    Bow,
    disinteractable
}
