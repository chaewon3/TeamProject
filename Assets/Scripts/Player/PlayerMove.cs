using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region ���� ����
    public float moveSpeed = 3f;
    CharacterController charCont;
    Animator playerAnimator;

    
    public bool canMove = true;
    public bool canRotat = true;

    public float dirSpeed; // ���߿� ����� 

    float gravity = -3f;
    bool isDush = false;
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
    //public List<GameObject> textWeapon = new List<GameObject>();
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
            if(!isDush)
            {
                moveDir.z = Input.GetAxisRaw("Vertical");
                moveDir.x = Input.GetAxisRaw("Horizontal");
            }
            else
            {
                moveDir.z = 1f;
                moveDir.x = 0f;
            }
            

            charCont.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
            charCont.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);

            playerAnimator.SetFloat("Xposition", moveDir.x);
            playerAnimator.SetFloat("Yposition", moveDir.z);
            playerAnimator.SetFloat("Speed", moveDir.magnitude);
            #endregion

            #region ���
            if (Input.GetKeyDown(KeyCode.Space) && dushCoroutine == null)// isground ���� �߰�?
            {
                playerAnimator.SetTrigger("Dush");
                dushCoroutine = StartCoroutine(Dush());
                //if (dushCoroutine == null)
                    
            }
            #endregion //���� ����Ű�� ���� ĳ���� ȸ��? �ٸ� �ִϸ��̼� ã��?
        }

        #region �߷� 

        isGrounded = Physics.Raycast(transGroundCheckPoint.position, Vector3.down, 0.2f, groundMask);
        Debug.DrawRay(transGroundCheckPoint.position - Vector3.down * 0.05f, Vector3.down, Color.red ,0.2f);

        if (!isGrounded)
        {
            charCont.Move(transform.up * gravity * Time.deltaTime);
        }
        #endregion

        if(canRotat)
        {
            #region ���콺 ���� ȸ��

            dirX += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
            dirY -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

            dirY = Mathf.Clamp(dirY, -90f, 90f);
            transform.localRotation = Quaternion.Euler(0, dirX * dirSpeed, 0f);
            #endregion
        }

        StateController(state); //�̰� ��� ȣ���ϴ°� ���� �̻��� ���� ����?
    }

    IEnumerator Dush()
    {
        moveSpeed = 6;
        isDush = true;
        charCont.height = 1f;
        charCont.center = new Vector3(0, 0.54f, 0);
        yield return new WaitForSeconds(0.8f); //AniClip.Length = 0.833f

        moveSpeed = 3;
        isDush = false;
        charCont.height = 1.88f;
        charCont.center = new Vector3(0, 0.93f, 0);
        dushCoroutine = null;
    }

    public void MoveLimit(bool bValue)
    {
        if(!bValue)
        {
            canMove = false;
            canRotat = false;
        }
        else
        {
            canMove = true;
            canRotat = true;
        }
    }

    void StateController(State state)
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
            case State.Town:
                playerAnimator.SetBool("BowForm", false);
                playerAnimator.SetBool("SwordForm", false);
                weapon[0].SetActive(false);
                weapon[1].SetActive(false);
                playerAnimator.SetLayerWeight(1, 0);
                break;
        }
    }
}

public enum State
{
    Sword,
    Bow,
    Town
}
