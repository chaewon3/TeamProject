using System.Collections;
using System.Collections.Generic;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        Description.Instance.DescriptionsON(item);

        if (eventData.clickCount == 2 && (Time.time - lastClickTime <= interval))
        {
            // todo 장착 만들어야 함
            print("더블클릭");
        }
        else
            lastClickTime = Time.time;
    }

}
