using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDamage;
    public Image itemIcon;
    public TextMeshProUGUI itemDesc;

    public static Description Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DescriptionsON(ItemData item)
    {
        gameObject.SetActive(true);

        itemName.text = item.Data.name;
        if (item is EquipmentData)
        {
            print("됨");
            itemDamage.enabled = true;
            var equip = (EquipmentData)item;
            if (equip.DataEquip.equiptype == EquipType.Armor)
            {
                itemDamage.text = $"+{equip.DataEquip.DEF + equip.additionalAbility} 체력";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                if (equip.Data.type == ItemType.Equipment_BOW)
                    sb.Append("원거리 피해 ");
                else if (equip.Data.type == ItemType.Equipment_SWORD)
                    sb.Append("근거리 피해");
                sb.Append(equip.DataEquip.ATK + equip.additionalAbility);
                itemDamage.text = sb.ToString();
            }
        }
        else
            itemDamage.enabled = false;
        itemIcon.sprite = item.Data.icon;
        itemDesc.text = item.Data.description;
    }

    public void DescriptionOFF()
    {
        gameObject.SetActive(false);
    }
}
