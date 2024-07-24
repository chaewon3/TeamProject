using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;


public class Notification : MonoBehaviour
{
    public GameObject Player;
    public CinemachineVirtualCamera CamStart;
    public CinemachineVirtualCamera CamEnd;
    public AnimationClip StandUp;
    public GameObject Stool;

    public CanvasGroup OptionUI;
    public GameObject NotionUI;
    public TextMeshProUGUI notion;

    private int option;

    private void Start()
    {
        if(!GameManager.Instance.gamestart)
        {
            Player.GetComponent<Animator>().SetLayerWeight(2, 1);
            CamStart.Priority = 3;
            StartCoroutine(UIFadeIn());
            GameManager.Instance.gamestart = true;
        }
        else
        {
            CamStart.Priority = 3;
            CamEnd.Priority = 3;
            OptionUI.alpha = 0;
            OptionUI.interactable = false;
            Player.GetComponent<CharacterController>().enabled = true;
            CanvasManager.ShowPlayer();

        }
    }

    public void Notify(int option)
    {
        this.option = option;
        if (option == 0)
            notion.text = "게임을 종료하시겠습니까?";
        else if (option == 1)
            notion.text = "진행 상황이 사라질 수 있습니다.\n 새로운 데이터로 진행하시겠습니까?";
        NotionUI.SetActive(true);
    }

    public void YES()
    {
        if(option == 0)
        {
            DataManager.Instance.SaveData();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
        else if(option == 1)
            StartCoroutine(gamestart());
    }

    public void NO()
    {
        if (GameManager.Instance.gamestart)
            CanvasManager.Restart();
        NotionUI.SetActive(false);
    }

    public void Continue()
    {
        DataManager.Instance.LoadData();
        StartCoroutine(gamestart());
    }

    IEnumerator gamestart()
    {
        NotionUI.SetActive(false);
        OptionUI.alpha = 0;
        OptionUI.interactable = false;
        Player.GetComponent<Animator>().SetTrigger("StandUp");
        yield return new WaitForSeconds(0.4f);
        CamEnd.Priority = 3;
        yield return new WaitForSeconds(StandUp.length);
        Stool.transform.position = new Vector3(0.6f, 0, 0.25f);
        Player.GetComponent<CharacterController>().enabled = true;
        Player.GetComponent<Animator>().SetLayerWeight(2, 0);
        yield return new WaitForSeconds(0.3f);
        CanvasManager.ShowPlayer();
        notion.text = "게임을 종료하시겠습니까?";
        option = 0;
    }

    IEnumerator UIFadeIn()
    {
        float starttime = 0;
        while(starttime <3.5f)
        {
            float t = (Time.time - starttime) / 5f;
            OptionUI.alpha = Mathf.Lerp(0, 1, starttime/5);
            starttime += Time.deltaTime;
            yield return null;
        }
        GameManager.Instance.MouseLock(false);
        OptionUI.alpha = 1;
    }
}
