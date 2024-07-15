using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region 전역 변수
    public float moveSpeed = 3f;
    CharacterController charCont;
    Animator playerAnimator;
    bool canMove = true;

    public float dirSpeed; // 나중에 지울거 

    float gravity = -3f;
    bool isGrounded;
    LayerMask groundMask;
    Transform transGroundCheckPoint;

    float mouseSensitivity = 200f;
    float dirX;
    float dirY;

    public State state;
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
        }

        StateController(state);
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
        switch (state)
        {
            case State.Sword:
                playerAnimator.SetBool("BowForm", false);
                playerAnimator.SetBool("SwordForm", true);
                break;
            case State.Bow:
                playerAnimator.SetBool("SwordForm", false);
                playerAnimator.SetBool("BowForm", true);
                break;
        }
    }
}

public enum State
{
    Sword,
    Bow,
    Incentory
}
