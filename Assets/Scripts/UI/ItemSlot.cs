using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    private Image icon;
    private TextMeshProUGUI amount;
    public ItemData item;

    float interval = 0.25f;
    float lastClickTime = 0;

    private void Awake()
    {
        icon = transform.Find("Item Icon").GetComponent<Image>();
        amount = transform.Find("amount").GetComponent<TextMeshProUGUI>();
    }

    public void setItem(ItemData item)
    {
        this.item = item;
        icon.enabled = true;
        icon.sprite = item.Data?.icon;
        if (item is ConsumableData)
        {
            amount.text = item.amount.ToString();
            amount.enabled = true;
        }
        else
            amount.enabled = false;
    }

    public void Clear()
    {
        item = null;
        icon.enabled = false;
        amount.enabled = false;
    }

    public void Equip(int slot)
    {
        UISound.Instance.Equip();
        if (InventoryManager.Equips[slot] == null)
        {
            PlayerManager.Instance.EquipChange(slot, item.tableID);
            InventoryManager.Equips[slot] = item;
            InventoryManager.Items.Remove(item);

            EquipmentData tmep = item as EquipmentData;

            if(slot == 0 && slot ==2)
                PlayerManager.Instance.EquipAddStats(slot, tmep.DataEquip.ATK + tmep.additionalAbility);
            else
                PlayerManager.Instance.EquipAddStats(slot, tmep.DataEquip.DEF + tmep.additionalAbility);

        }
        else
        {
            PlayerManager.Instance.EquipChange(slot, item.tableID);
            int index = InventoryManager.Items.FindIndex(a => a.UniqueID == item.UniqueID);
            ItemData temp = item;
            item = InventoryManager.Equips[slot];
            InventoryManager.Items[index] = item;
            InventoryManager.Equips[slot] = temp;

            EquipmentData tmepItem = item as EquipmentData;

            if (slot == 0 && slot == 2)
            {
                PlayerManager.Instance.EquipSubStats(slot, tmepItem.DataEquip.ATK + tmepItem.additionalAbility);
                PlayerManager.Instance.EquipAddStats(slot, tmepItem.DataEquip.ATK + tmepItem.additionalAbility);
            }    
            else
            {
                PlayerManager.Instance.EquipSubStats(slot, tmepItem.DataEquip.DEF + tmepItem.additionalAbility);
                PlayerManager.Instance.EquipAddStats(slot, tmepItem.DataEquip.DEF + tmepItem.additionalAbility);
            }     
        }
        InventoryManager.Refresh();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item == null)
            return;
        Description.Instance.DescriptionsON(item);
        if (eventData.clickCount == 2 && (Time.time - lastClickTime <= interval))
        {            
            switch(item.Data.type)
            {
                case ItemType.Equipment_ARMOR:
                    Equip(1);
                    break;
                case ItemType.Equipment_BOW:
                    Equip(2);
                    break;
                case ItemType.Equipment_SWORD:
                    Equip(0);
                    break;
                case ItemType.Artifact:
                    for(int i =0; i<3;i++)
                    {
                        if (InventoryManager.Artifact[i] == null)
                        {
                            InventoryManager.Artifact[i] = item;
                            InventoryManager.Refresh(); break;
                        }
                        else
                        {
                            if (InventoryManager.Artifact[i].tableID == item.tableID)
                            {
                                Description.Instance.Window("이미 장착된 아이템입니다.");
                                break;
                            }
                        }
                    }
                    break;
                default:break;
            }
        }
        else
            lastClickTime = Time.time;
    }
}
