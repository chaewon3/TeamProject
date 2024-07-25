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
    AudioSource audioSource;
    public AudioClip[] audioClip;

    private void Awake()
    {
        StartCoroutine(LoadScene());
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        string name = GameManager.Instance.currentScene;
        switch(name)
        {
            case "MainScene":
                SceneName.text = "야영지";
                image.sprite = spr[0];
                break;
            case "DungeonScene":
                SceneName.text = "무너진 광산";
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
        yield return new WaitForSeconds(0.5f);
        if(name == "MainScene")
        {
            audioSource.loop = false;
            audioSource.clip = audioClip[0];
            audioSource.Play();
        }
        else
        {

            audioSource.loop = true;
            audioSource.clip = audioClip[1];
            audioSource.Play();
        }
        yield return new WaitForSecondsRealtime(3.5f);
        audioSource.Stop();

        yield return new WaitForSecondsRealtime(1f);
        op.allowSceneActivation = true;
    }
}
