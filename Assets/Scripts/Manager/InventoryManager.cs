using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<ItemData> items = new List<ItemData>();
    public int[] equipSlots = new int[3];
    public int[] ArtifactsSlots = new int[3];

    public static InventoryPanel Panel => CanvasManager.inventoryPanel;
    public static InventoryManager Instance { get; private set; }

    // 저장을 위한 함수
    public static List<ItemData> Items { get => Instance.items; set => Instance.items = value; }

    private void Awake()
    {
        Instance = this;
    }
    public static void AddItem(ItemData item)
    {
        if (item is EquipmentData)
        {
            Instance.items.Add(item);
        }
        else if (item is ConsumableData)
        {

            foreach (ItemData data in Items)
            {
                if (data.tableID == item.tableID)
                {
                    if (data.amount < 99)
                    {
                        data.amount++;
                        return;
                    }
                }
            }
            Instance.items.Add(item);
        }
    }
    
    /// <summary>
    /// 인벤토리 새로고침
    /// </summary>
    public static void Refresh()
    {
        if (Panel == null)
            print("1");
        Panel.Refresh(Instance.items);
    }
}
