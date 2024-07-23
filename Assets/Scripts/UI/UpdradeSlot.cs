using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpdradeSlot : MonoBehaviour,IPointerUpHandler, IPointerClickHandler
{
    private GameObject Lock;
    private GameObject Upgrade;
    EquipmentData item;

    private bool isUpgrade;

    [Range(1,3)]
    public int slotLevel;

    private void Awake()
    {
        Lock = transform.Find("Lock").gameObject;
        Upgrade = transform.Find("Upgrade").gameObject;
    }

    private void OnEnable()
    {
        item = Description.Instance.currentItem;
        if(item.upgrade[slotLevel] == 0)
        {
            Lock.SetActive(true);
            Upgrade.SetActive(false);
        }
        else
        {
            Lock.SetActive(false);
            Upgrade.SetActive(true);
        }
    }

    /// <summary>
    /// ��ȭ�ϴµ� �ʿ��� ��ų����Ʈ Ȥ�� ��ȭ�� ������ �󸶳� �پ�����
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
