using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector]
    public string currentScene;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void MouseLock(bool Lock)
    {
        if (Lock)
            Cursor.lockState = CursorLockMode.Locked;
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void LoadScene(string scenename)
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentScene = scenename;
        SceneManager.LoadScene("LoadingScene");
    }

   
}
