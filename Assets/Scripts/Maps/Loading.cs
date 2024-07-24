using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public TextMeshProUGUI SceneName;
    public Sprite[] spr;
    public Image image;

    private void Awake()
    {
        StartCoroutine(LoadScene());
    }

    private void Start()
    {
        string name = GameManager.Instance.currentScene;
        switch(name)
        {
            case "MainScene":
                SceneName.text = "�߿���";
                image.sprite = spr[0];
                break;
            case "Dungeon":
                SceneName.text = "������ ����";
                image.sprite = spr[1];
                break;
        }    
    }

    IEnumerator LoadScene()
    {
        string name = GameManager.Instance.currentScene;
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(name);
        op.allowSceneActivation = false;
        yield return new WaitForSecondsRealtime(5f);
        op.allowSceneActivation = true;
    }
}
