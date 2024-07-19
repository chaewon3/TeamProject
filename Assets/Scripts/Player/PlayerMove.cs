using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region ���� ����
    public float moveSpeed = 3f;
    CharacterController charCont;
    Animator playerAnimator;
    public bool canMove = false;

    public float dirSpeed; // ���߿� ����� 

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
            #region �̵�
         
            Vector3 moveDir = Vector3.zero;
            moveDir.z = Input.GetAxisRaw("Vertical");
            moveDir.x = Input.GetAxisRaw("Horizontal");

            charCont.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
            charCont.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);

            playerAnimator.SetFloat("Xposition", moveDir.x);
            playerAnimator.SetFloat("Yposition", moveDir.z);
            playerAnimator.SetFloat("Speed", moveDir.magnitude);
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
            transform.localRotation = Quaternion.Euler(0, dirX * dirSpeed, 0f);
            #endregion

            #region ���
            if (Input.GetKeyDown(KeyCode.Space))// isground ���� �߰�?
            {
                print("�����ϴ�");
                playerAnimator.SetTrigger("Dush"); 

                if(dushCoroutine == null)
                    dushCoroutine = StartCoroutine(Dush());
            }
            #endregion //���� ����Ű�� ���� ĳ���� ȸ��? �ٸ� �ִϸ��̼� ã��?
        }

        StateController(state); //�̰� ��� ȣ���ϴ°� ���� �̻��� ���� ����?
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
