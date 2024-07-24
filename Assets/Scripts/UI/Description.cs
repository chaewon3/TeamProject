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

    public CanvasGroup Error;
    public TextMeshProUGUI ErrorMessgae;

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
        itemName.text = item.Data.name;
        if (item is EquipmentData)
        {
            currentItem = (EquipmentData)item;
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
        if(currentItem.upgrade[slotLevel] != 0)
        {
            ErrorMessgae.text = "이미 강화된 슬롯입니다.";
            StopAllCoroutines();
            StartCoroutine(Warning());
            return;
        }
        // 앞의 슬롯이 업그레이드 되었는지 확인
        for (int i = 0; i<slotLevel;i++)
        {
            if (currentItem.upgrade[i] == 0)
                upgrade = false;
        }        

        if(!upgrade)
        {
            //앞슬롯 강화 경고창
            ErrorMessgae.text = "아직 강화할 수 없는 슬롯입니다.";
            StopAllCoroutines();
            StartCoroutine(Warning());
            return;
        }

        if(currentItem.upgrade[2] != 0)
        {
            ErrorMessgae.text = "최대 강화 수치입니다.";
            StopAllCoroutines();
            StartCoroutine(Warning());
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
            PlayerManager.Data.skillPoint -= slotLevel + 1;
            ErrorMessgae.text = $"강화에 성공했습니다. +(<color=green>{additionDam}</color>)";
        }
        else
        {
            ErrorMessgae.text = $"스킬 포인트가 {slotLevel+1}필요합니다..";
        }
        StopAllCoroutines();
        StartCoroutine(Warning());
        InventoryManager.Refresh();
        DescriptionsON(currentItem);
    }
    public void Window(string str)
    {
        ErrorMessgae.text = str;
        StopAllCoroutines();
        StartCoroutine(Warning());
    }

    IEnumerator Warning()
    {
        UISound.Instance.Notion();
        Error.alpha = 0;
        float starttime = 0;
        while (starttime < 0.5f)
        {
            float t = (Time.time - starttime) / 0.5f;
            Error.alpha = Mathf.Lerp(0, 1, starttime / 0.5f);
            starttime += Time.deltaTime;
            yield return null;
        }
        Error.alpha = 1;
        yield return new WaitForSeconds(0.8f);
        starttime = 0;
        while (starttime < 0.5f)
        {
            float t = (Time.time - starttime) / 0.5f;
            Error.alpha = Mathf.Lerp(1,0, starttime / 0.5f);
            starttime += Time.deltaTime;
            yield return null;
        }
        Error.alpha = 0;
    }

}
