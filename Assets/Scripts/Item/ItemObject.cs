using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemDataSO itemData;

    public ItemData item { get; set; }

    private void Start()
    {
        if(item == null)
        {
            if (itemData is EquipmentDataSO)
                item = new EquipmentData(itemData as EquipmentDataSO);
            else if (itemData is ConsumableDataSO)
                item = new ConsumableData(itemData as ConsumableDataSO);
        }
    }

    private void Update()
    {
        //빙글 돌아가거나 파티클 생성    
    }

    public void interaction(bool OnOff)
    {
        StartCoroutine(Anim());
        InventoryManager.AddItem(item);
        InventoryManager.Refresh();
    }

    IEnumerator Anim()
    {
        float starttime = 0;
        while (starttime < 0.1f)
        {
            float t = (Time.time - starttime) / 0.1f;
            transform.localScale = Vector3.Lerp(new Vector3(1,1,1), new Vector3(1.05f, 1.05f, 1.05f), starttime / 0.1f);
            starttime += Time.deltaTime;
            yield return null;
        }
        starttime = 0;
        while (starttime < 0.5f)
        {
            float t = (Time.time - starttime) / 0.5f;
            transform.localScale = Vector3.Lerp(new Vector3(1.05f, 1.05f, 1.05f), Vector3.zero, starttime / 0.5f);
            starttime += Time.deltaTime;
            yield return null;
        }
    }
}
