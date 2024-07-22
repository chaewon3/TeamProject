using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerUpHandler
{
    private Image icon;
    public ItemData item;

    bool isDoubleClick;
    float interval = 0.25f;
    float doubleClickTime = -1;

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
        // 아이템 정보 보여주는 로직

        if(isDoubleClick)
        {
            //장착
            isDoubleClick = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if ((Time.time - doubleClickTime) < interval)
        {
            isDoubleClick = true;
            doubleClickTime = -1;
        }
        else
        {
            isDoubleClick = false;
            doubleClickTime = Time.time;
        }
    }
}
