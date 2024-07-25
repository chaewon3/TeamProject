using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    private InventoryPanel InvenUI;
    public PlayerPanel PlayerUI;
    private RectTransform CursorUI;
    private RectTransform MainOption;
    private RectTransform notion;

    private GameObject deadUI;
    [HideInInspector]
    public GameObject BossHPBar;

    public static InventoryPanel inventoryPanel => Instance.InvenUI;
    public static PlayerPanel PalyerPanel => Instance.PlayerUI;

    private void Awake()
    {
        Instance = this;
        InvenUI = transform.Find("InventoryUI").GetComponent<InventoryPanel>();
        PlayerUI = transform.Find("PlayerUI").GetComponent<PlayerPanel>();
        MainOption = transform.Find("MainOptions").GetComponent<RectTransform>();
        BossHPBar = transform.Find("BossHPBarCanvas").gameObject;
        CursorUI = transform.Find("Cursor").GetComponent<RectTransform>();
        notion = transform.Find("Notification ").GetComponent<RectTransform>();
        deadUI = transform.Find("DeadUI").gameObject;
    }

    private void Start()
    {
        PlayerUI.gameObject.SetActive(false);
        InvenUI.gameObject.SetActive(false);
        BossHPBar.SetActive(false);

        if(!GameManager.Instance.gamestart)
           MainOption.gameObject.SetActive(true);

    }

    public static void ShowInentory()
    {
        GameManager.Instance.CanMove(false);
        GameManager.Instance.MouseLock(false);
        Instance.InvenUI.gameObject.SetActive(true);
        Instance.PlayerUI.gameObject.SetActive(false);
        Instance.CursorUI.gameObject.SetActive(false);
    }

    public static void ShowPlayer()
    {
        GameManager.Instance.CanMove(true);
        GameManager.Instance.MouseLock(true);
        Instance.InvenUI.gameObject.SetActive(false);
        Instance.PlayerUI.gameObject.SetActive(true);
        Instance.CursorUI.gameObject.SetActive(true);
    }

    public static void ShowBossHPBar()
    {
        Instance.BossHPBar.GetComponentInChildren<UnityEngine.UI.Slider>().value = 1;
        Instance.BossHPBar.SetActive(true);
    }

    public static void ShowMap()
    {
        GameManager.Instance.CanMove(false);
        GameManager.Instance.MouseLock(false);
        Instance.PlayerUI.gameObject.SetActive(false);
        Instance.CursorUI.gameObject.SetActive(false);
        Instance.InvenUI.gameObject.SetActive(false);
    }

    public static void Exit()
    {
        UISound.Instance.Notion(); 
        Time.timeScale = 0;
        GameManager.Instance.MouseLock(false);
        Instance.notion.gameObject.SetActive(true);
        Instance.PlayerUI.gameObject.SetActive(false);
        Instance.CursorUI.gameObject.SetActive(false);
    }

    public static void Restart()
    {
        Time.timeScale = 1;
        GameManager.Instance.MouseLock(true);
        Instance.notion.gameObject.SetActive(false);
        Instance.PlayerUI.gameObject.SetActive(true);
        Instance.CursorUI.gameObject.SetActive(true);
    }

    public void PlayerDead()
    {
        deadUI.SetActive(true);
    }
}
