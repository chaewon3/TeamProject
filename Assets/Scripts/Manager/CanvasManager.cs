using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    private InventoryPanel InvenUI;
    private RectTransform PlayerUI;
    private RectTransform MainOption;

    public static InventoryPanel inventoryPanel => Instance.InvenUI;

    private void Awake()
    {
        Instance = this;
        InvenUI = transform.Find("InventoryUI").GetComponent<InventoryPanel>();
        //PlayerUI = transform.Find("PlayerUI").GetComponent<RectTransform>();
        MainOption = transform.Find("MainOptions").GetComponent<RectTransform>();
    }

    private void Start()
    {
        //PlayerUI.gameObject.SetActive(false);
        InvenUI.gameObject.SetActive(true);
        MainOption.gameObject.SetActive(false);
    }
    //todo ���߿� MainScene���� ���� �� �ٲٳ�
    public static void ShowInentory()
    {
        Instance.InvenUI.gameObject.SetActive(true);
        //Instance.PlayerUI.gameObject.SetActive(false);
    }

    public static void ShowPlayer()
    {
        Instance.InvenUI.gameObject.SetActive(false);
        //Instance.PlayerUI.gameObject.SetActive(true);
    }

}
