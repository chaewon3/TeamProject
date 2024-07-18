using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StartScene : MonoBehaviour
{
    public PlayerMove Move;
    public GameObject Player;
    public CinemachineVirtualCamera Cam1;
    public CinemachineVirtualCamera Cam2;
    public AnimationClip StandUp;

    private void Awake()
    {
        // �ӽ� ���߿� canMove��ü�� ���� ��
        Move.dirSpeed = 0;
    }
    private void Start()
    {
        Cam1.Priority = 3;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(gamestart());
        }
    }

    // todo ���� ��򰡿��� ������ �ڷ� �о�� �ҵ�?
    // ���� �ִϸ��̼� �� �̻���
    IEnumerator gamestart()
    {
        Player.GetComponent<Animator>().SetTrigger("StandUp");
        yield return new WaitForSeconds(0.2f);
        Cam2.Priority = 3;
        yield return new WaitForSeconds(StandUp.length);
        Player.GetComponent<CharacterController>().enabled = true;
        Player.GetComponent<Animator>().SetLayerWeight(2, 0);
        Move.dirSpeed = 2;
        Destroy(this);
    }
}
