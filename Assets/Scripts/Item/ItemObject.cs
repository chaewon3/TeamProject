using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemDataSO itemData;
    private GameObject item;
    private PlayerSound sound;

    public ItemData Item { get; set; }

    private void Awake()
    {
        item = transform.Find("Item").gameObject;
        sound = FindAnyObjectByType<PlayerSound>();

    }
    private void Start()
    {
        if(Item == null)
        {
            if (itemData is EquipmentDataSO)
                Item = new EquipmentData(itemData as EquipmentDataSO);
            else if (itemData is ConsumableDataSO)
                Item = new ConsumableData(itemData as ConsumableDataSO);
        }
    }

    private void Update()
    {
        transform.Rotate(transform.up * 50f * Time.deltaTime);
    }

    public void interaction(bool OnOff)
    {
        sound.getItem();
        StartCoroutine(Anim());
        InventoryManager.AddItem(Item);
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
        Destroy(this.gameObject);
    }
}
