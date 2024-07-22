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
        icon.enabled = false;
        defaulticon.enabled = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (item == null)
            return;
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            InventoryManager.AddItem(item);
            switch(item.Data.type)
            {
                case ItemType.Equipment_ARMOR:
                    InventoryManager.Equips[1] = null;
                    break;
                case ItemType.Equipment_BOW:
                    InventoryManager.Equips[2] = null;
                    break;
                case ItemType.Equipment_SWORD:
                    InventoryManager.Equips[0] = null;
                    break;
                case ItemType.Artifact:
                    break;
                default: break;
            }
            Clear();
            InventoryManager.Refresh();
        }
    }
}
