using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    PlayerMove Player;

    [HideInInspector]
    public string currentScene;
    public bool gamestart = false;

    private void Awake()
    {
        Player = FindObjectOfType<PlayerMove>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (Instance == null)
        {
            CanMove(false);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void CanMove(bool move)
    {
        Player.canMove = move;
        Player.canRotat = move;
    }

    public void MouseLock(bool Lock)
    {
        if (Lock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public void LoadScene(string scenename)
    {
        DataManager.Instance.SaveData();
        Cursor.lockState = CursorLockMode.Locked;
        currentScene = scenename;
        SceneManager.LoadScene("LoadingScene");
    }

    public void GameOver(string scenename)
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentScene = scenename;
        SceneManager.LoadScene("LoadingScene");
    }
}
