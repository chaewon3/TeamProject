using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region 전역 변수
    float moveSpeed = 3f;
    CharacterController charCont;
    Animator PlayerAnimator;

    float gravity = -3f;
    bool isGrounded;
    LayerMask groundMask;
    Transform transGroundCheckPoint;

    float mouseSensitivity = 200f;
    float dirX;
    float dirY;
    #endregion


    private void Awake()
    {
        charCont = GetComponent<CharacterController>();
        PlayerAnimator = GetComponent<Animator>();

        transGroundCheckPoint = transform;
        groundMask = (1 << LayerMask.NameToLayer("Ground"));
    }

    private void Update()
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.z = Input.GetAxisRaw("Vertical");
        moveDir.x = Input.GetAxisRaw("Horizontal");

        charCont.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
        charCont.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);

        PlayerAnimator.SetFloat("Xposition", moveDir.x);
        PlayerAnimator.SetFloat("Yposition", moveDir.z);
        PlayerAnimator.SetFloat("Speed", moveDir.magnitude);

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
        transform.localRotation = Quaternion.Euler(0, dirX, 0f);
        #endregion

    }
}

//float z = Input.GetAxisRaw("Vertical");
//charCont.Move(transform.forward * z * moveSpeed * Time.deltaTime);
//if (z != 0)
//{
//    PlayerAnimator.SetFloat("Speed", 1f);
//}

//float x = Input.GetAxisRaw("Horizontal");
//charCont.Move(transform.right * x * moveSpeed * Time.deltaTime);