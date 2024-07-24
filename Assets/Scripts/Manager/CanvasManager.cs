using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    private InventoryPanel InvenUI;
    private PlayerPanel PlayerUI;
    private RectTransform MainOption;

    public static InventoryPanel inventoryPanel => Instance.InvenUI;
    public static PlayerPanel PalyerPanel => Instance.PlayerUI;

    private void Awake()
    {
        Instance = this;
        InvenUI = transform.Find("InventoryUI").GetComponent<InventoryPanel>();
        PlayerUI = transform.Find("PlayerUI").GetComponent<PlayerPanel>();
        MainOption = transform.Find("MainOptions").GetComponent<RectTransform>();
    }

    private void Start()
    {
        PlayerUI.gameObject.SetActive(false);
        InvenUI.gameObject.SetActive(false);
        MainOption.gameObject.SetActive(true);
    }

    //todo ���߿� MainScene���� ĵ���� Ű�� ���� ���� �� �ٲٳ�
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

}
