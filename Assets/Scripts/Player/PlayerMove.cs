using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region 전역 변수
    float moveSpeed = 3f;
    CharacterController charCont;
    Vector3 playerPos;
    Vector3 movePos;


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
        
        transGroundCheckPoint = transform;
        groundMask = (1 << LayerMask.NameToLayer("Ground"));
    }

    private void Update()
    {
        float z = Input.GetAxisRaw("Vertical");
        charCont.Move(transform.forward * z * moveSpeed * Time.deltaTime);

        float x = Input.GetAxisRaw("Horizontal");
        charCont.Move(transform.right * x * moveSpeed * Time.deltaTime);

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
        //Vector3 mousePos = Input.mousePosition;

        //Ray ray = Camera.main.ScreenPointToRay(mousePos);

        //if (Physics.Raycast(ray, out var hit, 100, groundMask))
        //{
        //    playerPos = hit.point;
        //};

        ////transform.LookAt(playerPos);
        //transform.rotation = Quaternion.LookRotation(playerPos, Vector3.up);
        #endregion

    }
}


/*
         float z = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");

        movePos = transform.position;
        movePos += transform.forward * z;
        movePos += transform.right * x;

        charCont.Move(movePos * Time.deltaTime);
 */