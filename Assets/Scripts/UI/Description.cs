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
    public GameObject Upgrade;
    public UpdradeSlot[] upgradeSlot = new UpdradeSlot[3];
    //public GameObject tooltip;

    public static Description Instance;

    private EquipmentData currentItem;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DescriptionOFF();
    }

    public void DescriptionsON(ItemData item)
    {
        gameObject.SetActive(true);
        currentItem = (EquipmentData)item;

        itemName.text = item.Data.name;
        if (item is EquipmentData)
        {
            itemDamage.enabled = true;
            Upgrade.SetActive(true);
            var equip = (EquipmentData)item;
            for( int i = 0; i<3; i++)
            {
                upgradeSlot[i].Set(equip);
            }
            if (equip.DataEquip.equiptype == EquipType.Armor)
            {
                itemDamage.text = $"+{equip.DataEquip.DEF + equip.additionalAbility} (<color=green>+{equip.additionalAbility}</color>) 체력";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                if (equip.Data.type == ItemType.Equipment_BOW)
                    sb.Append("원거리 피해 ");
                else if (equip.Data.type == ItemType.Equipment_SWORD)
                    sb.Append("근거리 피해 ");
                sb.Append(equip.DataEquip.ATK + equip.additionalAbility);
                sb.Append($"(<color=green>+{equip.additionalAbility}</color>)");
                itemDamage.text = sb.ToString();
            }
        }
        else
        {
            itemDamage.enabled = false;
            Upgrade.SetActive(false);
        }
        itemIcon.sprite = item.Data.icon;
        itemDesc.text = item.Data.description;
    }

    public void DescriptionOFF()
    {
        gameObject.SetActive(false);
    }

    public void upgrade(int slotLevel)
    {
        bool upgrade = true;
        // 앞의 슬롯이 업그레이드 되었는지 확인
        for(int i = 0; i<slotLevel;i++)
        {
            if (currentItem.upgrade[i] == 0)
                upgrade = false;
        }

        if(!upgrade)
        {
            //앞슬롯 강화 경고창
            return;
        }

        // 스킬포인트가 있는지 확인
        if (PlayerManager.Data.skillPoint >= (slotLevel + 1))
        {
            // 업그레이드 진행 후 리스트 아이템 변경
            int additionDam = Random.Range(20, 35);
            currentItem.Upgrade(additionDam, slotLevel);
            int index = InventoryManager.Items.FindIndex(a => a.UniqueID == currentItem.UniqueID);
            InventoryManager.Items[index] = currentItem;
        }
        else
        {
            print("스킬포인트가 부족합니다.");
        }
        upgradeSlot[slotLevel].Set(currentItem);
    }

    public void showtooltip(int slotLevel)
    {
        RectTransform transform = tooltip.GetComponent<RectTransform>();
        TextMeshProUGUI text = tooltip.GetComponent<TextMeshProUGUI>();
        StringBuilder sb = new StringBuilder();
        if(currentItem.upgrade[slotLevel] == 0)
        {
            //sb.Append
        }
        else
        {
            sb.Append($"+{currentItem.upgrade[slotLevel]}");
        }
        // 업그레이드 되었는지 안되었는지 확인
        // 안되었으면 몇 스킬포인트가 필요한지 /  앞의 슬롯을 열어야하는지
        // 되었으면 강화된 수치 표기
    }
}
