using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StartScene : MonoBehaviour
{
    public GameObject Player;
    public CinemachineVirtualCamera CamStart;
    public CinemachineVirtualCamera CamEnd;
    public AnimationClip StandUp;
    public GameObject Stool;
    public CanvasGroup UI;
    // index 1 : 메인 옵션 2: 경고창
    public GameObject[] UIs;

    private void Start()
    {
        CamStart.Priority = 3;
        StartCoroutine(UIFadeIn());
    }

    public void Confirm()
    {
        UIs[0].SetActive(false);
        UIs[1].SetActive(true);
    }

    public void NewGame()
    {
        StartCoroutine(gamestart());
    }

    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    IEnumerator gamestart()
    {
        UI.alpha = 0;
        UI.interactable = false;
        Player.GetComponent<Animator>().SetTrigger("StandUp");
        yield return new WaitForSeconds(0.4f);
        CamEnd.Priority = 3;
        yield return new WaitForSeconds(StandUp.length);
        Stool.transform.position = new Vector3(1.1f, -0.05f, -0.1f);
        Player.GetComponent<CharacterController>().enabled = true;
        Player.GetComponent<Animator>().SetLayerWeight(2, 0);
        yield return new WaitForSeconds(0.3f);
        GameManager.Instance.CanMove(true);
        GameManager.Instance.MouseLock(true);
        Destroy(this);
    }

    IEnumerator UIFadeIn()
    {
        float starttime = 0;
        while(starttime <3.5f)
        {
            float t = (Time.time - starttime) / 5f;
            UI.alpha = Mathf.Lerp(0, 1, starttime/5);
            starttime += Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.MouseLock(false);
        UI.alpha = 1;
    }
}
