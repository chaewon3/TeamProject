using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region ���� ����
    float moveSpeed = 3f;
    CharacterController charCont;
    Animator PlayerAnimator;
    bool canMove = true;

    float gravity = -3f;
    bool isGrounded;
    LayerMask groundMask;
    Transform transGroundCheckPoint;

    float mouseSensitivity = 200f;
    float dirX;
    float dirY;
    #endregion


    void Awake()
    {
        charCont = GetComponent<CharacterController>();
        PlayerAnimator = GetComponent<Animator>();

        transGroundCheckPoint = transform;
        groundMask = (1 << LayerMask.NameToLayer("Ground"));
    }

    void Update()
    {
        if(canMove)
        {
            #region �̵�
         
            Vector3 moveDir = Vector3.zero;
            moveDir.z = Input.GetAxisRaw("Vertical");
            moveDir.x = Input.GetAxisRaw("Horizontal");

            charCont.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
            charCont.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);

            PlayerAnimator.SetFloat("Xposition", moveDir.x);
            PlayerAnimator.SetFloat("Yposition", moveDir.z);
            PlayerAnimator.SetFloat("Speed", moveDir.magnitude);
            #endregion

            #region �߷� 

            isGrounded = Physics.Raycast(transGroundCheckPoint.position, Vector3.down, 0.2f, groundMask);

            if (!isGrounded)
            {
                charCont.Move(transform.up * gravity * Time.deltaTime);
            }
            #endregion

            #region ���콺 ���� ȸ��

            dirX += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
            dirY -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

            dirY = Mathf.Clamp(dirY, -90f, 90f);
            transform.localRotation = Quaternion.Euler(0, dirX, 0f);
            #endregion
        }
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
}