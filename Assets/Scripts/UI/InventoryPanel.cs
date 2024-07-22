using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    private List<ItemSlot> slots = new List<ItemSlot>();

    private void Awake()
    {
        slots.AddRange(GetComponentsInChildren<ItemSlot>());
    }

    public void Refresh(List<ItemData> itemList)
    {
        // ������ �� ���鼭 ������ ����Ʈ�� ����Ʈ�� �ִٸ� setitem�ϰ�, ����Ʈ�� ��� false�� ��ȯ�ȴٸ� ������ clear
        IEnumerator<ItemData> itemEnum = itemList.GetEnumerator();
        foreach(ItemSlot slot in slots)
        {
            if (itemEnum.MoveNext())
                slot.setItem(itemEnum.Current);
            else
                slot.Clear();
        }
    }
}
