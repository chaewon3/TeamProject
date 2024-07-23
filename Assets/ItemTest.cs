using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTest : MonoBehaviour
{
    private ItemDataSO itemSO;
    private ItemData item;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            int num = Random.Range(0,5);
            itemSO = DataManager.Instance.itemSOList[num];
            if (itemSO is EquipmentDataSO)
                item = new EquipmentData(itemSO as EquipmentDataSO);
            else
                item = new ConsumableData(itemSO as ConsumableDataSO);
            InventoryManager.AddItem(item);
            InventoryManager.Refresh();
        }
    }
}
