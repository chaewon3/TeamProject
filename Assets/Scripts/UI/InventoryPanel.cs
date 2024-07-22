using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType
{
    All,
    Weapon = ItemType.Equipment_BOW | ItemType.Equipment_SWORD,
    Armor = ItemType.Equipment_ARMOR,
    Consumable = ItemType.Consumable,
    Artifact = ItemType.Artifact
}

public class InventoryPanel : MonoBehaviour
{
    private List<ItemSlot> slots = new List<ItemSlot>();
    public EquipSlot[] equipslot = new EquipSlot[3];
    public EquipSlot[] ArtifactSlot = new EquipSlot[3];

    public SlotType type = SlotType.All;

    private void Awake()
    {
        slots.AddRange(GetComponentsInChildren<ItemSlot>());
    }

    public void SetType(int typenum)
    {
        switch(typenum)
        {
            case 0: type = SlotType.All; break;
            case 1: type = SlotType.Weapon; break;
            case 2: type = SlotType.Armor; break;
            case 3: type = SlotType.Consumable; break;
            case 4: type = SlotType.Artifact; break;
        }
        InventoryManager.Refresh();
    }

    public void Refresh(List<ItemData> itemList, ItemData[] equip, ItemData[] Artifact)
    {
        // 슬롯을 쭉 돌면서 아이템 리스트에 리스트가 있다면 setitem하고, 리스트가 비어 false가 반환된다면 슬롯을 clear
        IEnumerator<ItemData> itemEnum = itemList.GetEnumerator();
        foreach(ItemSlot slot in slots)
        {
            FindItem:
            if (itemEnum.MoveNext())
            {
                if (type == SlotType.All)
                    slot.setItem(itemEnum.Current);
                else if (((SlotType)itemEnum.Current.Data.type & type) != 0)
                    slot.setItem(itemEnum.Current);
                else
                    goto FindItem;
            }
            else
                slot.Clear();
        }
        for(int i=0;i<3;i++)
        {
            if (equip[i] == null)
                equipslot[i].Clear();
            else                
               equipslot[i].setItem(equip[i]);

            if (Artifact[i] == null)
                ArtifactSlot[i].Clear(); 
            else
                ArtifactSlot[i].setItem(Artifact[i]);
        }
    }
}
