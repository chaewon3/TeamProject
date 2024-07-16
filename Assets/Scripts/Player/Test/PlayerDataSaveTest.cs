using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSaveTest : MonoBehaviour
{
    PlayerData playerData;

    private void Awake()
    {
    }
    void Start()
    {
        //playerData = DataManager.Instance.PlayerFileLoad("PlayerInfo");
        //print($"PlayerDataSet => maxHP: {playerData.maxHP}, damage: {playerData.damage}, experience: {playerData.experience}");
        //print(DataManager.Instance.inventoryData.Items[1]);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //DataManager.Instance.PlayerInfoSave(100f, 10f, 0f);

            //playerData = DataManager.Instance.FileLoad("PlayerInfo");
            //print($"PlayerDataSet => maxHP: {playerData.maxHP}, damage: {playerData.damage}, experience: {playerData.experience}");
            ItemData item = Instantiate(ITemList.instance.ItemList[0]);
            item.price = 500;
            DataManager.Instance.inventoryData.Items.Add(1, item);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            print("save");
            DataManager.Instance.InventorySave();
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            print(DataManager.Instance.inventoryData.Items[1].price);
        }
    }
}
