using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    private InventoryPanel InvenUI;
    private RectTransform PlayerUI;
    private RectTransform MainOption;
    private GameObject BossHPBar;

    public static InventoryPanel inventoryPanel => Instance.InvenUI;

    private void Awake()
    {
        Instance = this;
        InvenUI = transform.Find("InventoryUI").GetComponent<InventoryPanel>();
        PlayerUI = transform.Find("PlayerUI").GetComponent<RectTransform>();
        MainOption = transform.Find("MainOptions").GetComponent<RectTransform>();
        BossHPBar = transform.Find("BossHPBarCanvas").gameObject;
    }

    private void Start()
    {
        PlayerUI.gameObject.SetActive(false);
        InvenUI.gameObject.SetActive(false);
        MainOption.gameObject.SetActive(true);
        BossHPBar.SetActive(false);
    }

    //todo 나중에 MainScene에서 캔버스 키고 끄는 연동 다 바꾸끼
    public static void ShowInentory()
    {
        GameManager.Instance.CanMove(false);
        GameManager.Instance.MouseLock(false);
        Instance.InvenUI.gameObject.SetActive(true);
        Instance.PlayerUI.gameObject.SetActive(false);
    }

    public static void ShowPlayer()
    {
        GameManager.Instance.CanMove(true);
        GameManager.Instance.MouseLock(true);
        Instance.InvenUI.gameObject.SetActive(false);
        Instance.PlayerUI.gameObject.SetActive(true);
    }

    public void ShowBossHPBar()
    {
        BossHPBar.GetComponentInChildren<UnityEngine.UI.Slider>().value = 1;
        BossHPBar.SetActive(true);
    }

}
