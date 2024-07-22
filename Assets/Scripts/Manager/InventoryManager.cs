using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<ItemData> items = new List<ItemData>();
    public ItemData[] equipSlots = new ItemData[3];
    public ItemData[] ArtifactsSlots = new ItemData[3];

    public static InventoryPanel Panel => CanvasManager.inventoryPanel;
    public static InventoryManager Instance { get; private set; }

    // ������ ���� �Լ�
    public static List<ItemData> Items { get => Instance.items; set => Instance.items = value; }

    private void Awake()
    {
        Instance = this;
    }
    public static void AddItem(ItemData item)
    {
        item.UniqueID = Items.Count;
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
    /// �κ��丮 ���ΰ�ħ
    /// </summary>
    public static void Refresh()
    {
        Panel.Refresh(Instance.items, Instance.equipSlots, Instance.ArtifactsSlots);
    }
}
