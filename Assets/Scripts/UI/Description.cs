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
                itemDamage.text = $"+{equip.DataEquip.DEF + equip.additionalAbility} (<color=green>+{equip.additionalAbility}</color>) ü��";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                if (equip.Data.type == ItemType.Equipment_BOW)
                    sb.Append("���Ÿ� ���� ");
                else if (equip.Data.type == ItemType.Equipment_SWORD)
                    sb.Append("�ٰŸ� ���� ");
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
        // ���� ������ ���׷��̵� �Ǿ����� Ȯ��
        for(int i = 0; i<slotLevel;i++)
        {
            if (currentItem.upgrade[i] == 0)
                upgrade = false;
        }

        if(!upgrade)
        {
            //�ս��� ��ȭ ���â
            return;
        }

        // ��ų����Ʈ�� �ִ��� Ȯ��
        if (PlayerManager.Data.skillPoint >= (slotLevel + 1))
        {
            // ���׷��̵� ���� �� ����Ʈ ������ ����
            int additionDam = Random.Range(20, 35);
            currentItem.Upgrade(additionDam, slotLevel);
            int index = InventoryManager.Items.FindIndex(a => a.UniqueID == currentItem.UniqueID);
            InventoryManager.Items[index] = currentItem;
        }
        else
        {
            print("��ų����Ʈ�� �����մϴ�.");
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
        // ���׷��̵� �Ǿ����� �ȵǾ����� Ȯ��
        // �ȵǾ����� �� ��ų����Ʈ�� �ʿ����� /  ���� ������ ������ϴ���
        // �Ǿ����� ��ȭ�� ��ġ ǥ��
    }
}
