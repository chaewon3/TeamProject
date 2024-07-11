using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 0f; // 10
    public float rotateSpeed = 10f;
    private float gravity = 20f;
    private Vector3 targetPos;
    private Vector3 playerPos;
    [SerializeField]
    private bool canMove = false;
    private CharacterController playerControllor;
    private Animator playerAnimator;

    private void Awake()
    {
        playerPos = Vector3.zero;
        playerControllor = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if(playerControllor.isGrounded)
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.tag == "Enemy")
                {
                    playerAnimator.SetTrigger("Attack");
                    return;
                }

                targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                //targetPos = hit.point;
                canMove = true;
                playerAnimator.SetFloat("Speed", 1f);
            }

            if (canMove)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

                if (transform.position == targetPos)
                {
                    canMove = false;
                }

            }
        }
        else
        {
            playerPos.y -= gravity * Time.deltaTime;

            playerControllor.Move(playerPos * Time.deltaTime);
        }

    }
}
