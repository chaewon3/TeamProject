using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSaveTest : MonoBehaviour
{
    PlayerData playerData;
    Inventory inven;

    private void Awake()
    {
    }
    void Start()
    {
        inven = DataManager.Instance.inventoryData;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //DataManager.Instance.PlayerInfoSave(100f, 10f, 0f);
            //playerData = DataManager.Instance.FileLoad("PlayerInfo");
            //print($"PlayerDataSet => maxHP: {playerData.maxHP}, damage: {playerData.damage}, experience: {playerData.experience}");
            int num = Random.Range(0, 6);
            //var json = JsonUtility.ToJson(ITemList.instance.ItemList[num]);
            //var clone = ScriptableObject.CreateInstance<ItemData>();
            //JsonUtility.FromJsonOverwrite(json, clone);
            ItemDataSO item = Instantiate(ITemList.instance.ItemList[num]) as ItemDataSO;
            print(item);
            DataManager.Instance.inventoryData.Items.Add(inven.Items.Count, item);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("save");
            DataManager.Instance.InventorySave();
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            print(DataManager.Instance.inventoryData.Items[1].name);
        }
    }
}
