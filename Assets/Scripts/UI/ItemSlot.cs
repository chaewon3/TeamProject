using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    private Image icon;
    public ItemData item;

    float interval = 0.25f;
    float lastClickTime = 0;

    private void Awake()
    {
        icon = transform.Find("Item Icon").GetComponent<Image>();
    }

    public void setItem(ItemData item)
    {
        this.item = item;
        icon.enabled = true;
        icon.sprite = item.Data?.icon;
    }

    public void Clear()
    {
        item = null;
        icon.enabled = false;
    }

    public void Equip(int slot)
    {
        if (InventoryManager.Instance.equipSlots[slot].tableID == 0)
        {
            InventoryManager.Instance.equipSlots[slot] = item;
            InventoryManager.Items.Remove(item);
            Clear(); 
        }
        else
        {
            int index = InventoryManager.Items.FindIndex(a => a.UniqueID == item.UniqueID);
            ItemData temp = item;
            item = InventoryManager.Instance.equipSlots[slot];
            InventoryManager.Items[index] = item;
            InventoryManager.Instance.equipSlots[slot] = temp;
        }
        InventoryManager.Refresh();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Description.Instance.DescriptionsON(item);
        if (eventData.clickCount == 2 && (Time.time - lastClickTime <= interval))
        {
            // todo ���� ������ ��
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
                        if(InventoryManager.Instance.ArtifactsSlots[i] == null)
                        {
                            InventoryManager.Instance.ArtifactsSlots[i] = item;
                            Clear(); break;
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
