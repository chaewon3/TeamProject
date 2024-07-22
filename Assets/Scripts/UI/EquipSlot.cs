using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EquipSlot : MonoBehaviour
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

}
