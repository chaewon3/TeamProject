using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    private List<ItemSlot> slots = new List<ItemSlot>();
    public EquipSlot[] equipslot = new EquipSlot[3];
    public EquipSlot[] ArtifactSlot = new EquipSlot[3];

    private void Awake()
    {
        slots.AddRange(GetComponentsInChildren<ItemSlot>());
    }

    public void Refresh(List<ItemData> itemList, ItemData[] equip, ItemData[] Artifact)
    {
        // 슬롯을 쭉 돌면서 아이템 리스트에 리스트가 있다면 setitem하고, 리스트가 비어 false가 반환된다면 슬롯을 clear
        IEnumerator<ItemData> itemEnum = itemList.GetEnumerator();
        foreach(ItemSlot slot in slots)
        {
            if (itemEnum.MoveNext())
                slot.setItem(itemEnum.Current);
            else
                slot.Clear();
        }
        for(int i=0;i<3;i++)
        {
            if (equip[i].tableID != 0)
                equipslot[i].setItem(equip[i]);
            else
                equipslot[i].Clear();

            if (Artifact[i].tableID != 0)
                ArtifactSlot[i].setItem(Artifact[i]);
            else
                ArtifactSlot[i].Clear();
        }
    }
}
