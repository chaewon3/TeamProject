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
    public GameObject Stool;

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

    IEnumerator gamestart()
    {
        Player.GetComponent<Animator>().SetTrigger("StandUp");
        yield return new WaitForSeconds(0.4f);
        Cam2.Priority = 3;
        yield return new WaitForSeconds(StandUp.length);
        Stool.transform.position = new Vector3(1.1f, -0.05f, -0.1f);
        Player.GetComponent<CharacterController>().enabled = true;
        Player.GetComponent<Animator>().SetLayerWeight(2, 0);
        yield return new WaitForSeconds(0.3f);
        GameManager.Instance.CanMove(true);
        Destroy(this);
    }
}
