using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpdradeSlot : MonoBehaviour
{
    private Image Lock;
    private Image Upgrade;

    [Range(0,2)]
    public int slotLevel;

    private void Awake()
    {
        Lock = transform.Find("Lock").GetComponent<Image>();
        Upgrade = transform.Find("Upgrade").GetComponent<Image>();
    }

    public void Set(EquipmentData item)
    {
        if (item.upgrade[slotLevel] == 0)
        {
            Lock.enabled = true;
            Upgrade.enabled = false;
        }
        else
        {
            Lock.enabled = false;
            Upgrade.enabled = true;
        }
    }

}
