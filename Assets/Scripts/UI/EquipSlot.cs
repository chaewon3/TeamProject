using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class EquipSlot : MonoBehaviour, IPointerDownHandler
{
    private Image icon;
    private Image defaulticon;
    public ItemData item;

    private void Awake()
    {
        icon = transform.Find("Item Icon").GetComponent<Image>();
        defaulticon = transform.Find("Default Icon").GetComponent<Image>();
    }

    public void setItem(ItemData item)
    {
        this.item = item;
        icon.enabled = true;
        defaulticon.enabled = false;
        icon.sprite = item.Data?.icon;
    }

    public void Clear()
    {
        item = null;
        defaulticon.enabled = true;
        icon.enabled = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (item == null)
            return;
        PlayerManager.Instance.TakeOFF(item.tableID);
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            int slot = 5;
            InventoryManager.AddItem(item);
            switch(item.Data.type)
            {
                case ItemType.Equipment_ARMOR:
                    InventoryManager.Equips[1] = null;
                    slot = 1;
                    break;
                case ItemType.Equipment_BOW:
                    InventoryManager.Equips[2] = null;
                    slot = 2;
                    break;
                case ItemType.Equipment_SWORD:
                    InventoryManager.Equips[0] = null;
                    slot = 0;
                    break;
                case ItemType.Artifact:
                    break;
                default: break;
            }

            EquipmentData tmepItem = item as EquipmentData;

            if (slot == 0 && slot == 2)
            {
                PlayerManager.Instance.EquipSubStats(slot, tmepItem.DataEquip.ATK + tmepItem.additionalAbility);
            }
            else if(slot == 1)
            {
                PlayerManager.Instance.EquipSubStats(slot, tmepItem.DataEquip.DEF + tmepItem.additionalAbility);
            }

            Clear();
            InventoryManager.Refresh();
        }
    }

}
