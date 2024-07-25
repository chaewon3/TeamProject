using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    private List<ItemData> items = new List<ItemData>();
    private ItemData[] equipSlots = new ItemData[3] { null, null, null };
    private ItemData[] ArtifactsSlots = new ItemData[3] { null, null, null };

    private int currentID = 1;
    public int Arrowcount { get; set; }


    public static InventoryPanel InventoryPanel => CanvasManager.inventoryPanel;
    public static PlayerPanel PlayerPanel => CanvasManager.PalyerPanel;
    public static InventoryManager Instance { get; private set; }

    // 저장을 위한 함수
    public static List<ItemData> Items { get => Instance.items; set => Instance.items = value; }
    public static ItemData[] Equips { get => Instance.equipSlots; set => Instance.equipSlots = value; }
    public static ItemData[] Artifact { get => Instance.ArtifactsSlots; set => Instance.ArtifactsSlots = value; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Set();
    }

    public static void AddItem(ItemData item)
    {        
        item.UniqueID = Instance.currentID;
        Instance.currentID++;
        if (item is EquipmentData)
        {
            Instance.items.Add(item);
        }
        else if (item is ConsumableData)
        {
            if (item.Data.type == ItemType.Consumable)
                Instance.Arrowcount += 5;

            foreach (ItemData data in Items)
            {
                if (data.tableID == item.tableID)
                {
                    if (data.amount < 99 && data is ConsumableData consumdata)
                    {
                        data.amount += consumdata.stack;
                        return;
                    }
                }
            }
            Instance.items.Add(item);
        }
    }
    
    public static void UseItem(ItemData item, int slot)
    {
        if (item is not ConsumableData)
            return;
        item.amount -= 1;
        if (item.amount <= 0)
        {
            Artifact[slot] = null;
            Items.Remove(item);
        }
        else
            Artifact[slot] = item;

        Refresh();
    }

    public static void UseArrow()
    {
        ItemData item;
        item = Items.FindLast(item => item.tableID == 101);
        item.amount -= 1;
        Instance.Arrowcount--;
        if (item.amount <= 0)
            Items.Remove(item);
        Refresh();
    }

    /// <summary>
    /// 인벤토리 새로고침
    /// </summary>
    public static void Refresh()
    {
        InventoryPanel.Refresh(Instance.items, Instance.equipSlots, Instance.ArtifactsSlots);
        PlayerPanel.Referesh(Instance.ArtifactsSlots);
    }

    public void Set()
    {
        List<ItemData> items;
        items = Items.FindAll(item => item.tableID == 101);
        if (items == null)
            return;
        foreach(ItemData item in items)
        {
            Instance.Arrowcount += item.amount;
        }
    }
}
