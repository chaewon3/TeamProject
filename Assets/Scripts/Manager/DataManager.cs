using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileData
{
    public PlayerData PlayerData;
    public List<EquipmentData> EquipData;
    public List<ConsumableData> consumData;

    public FileData()
    {
        PlayerData = new PlayerData();
        EquipData = new List<EquipmentData>();
        consumData = new List<ConsumableData>();
    }

    public void AddItem(ItemData item)
    {
        if (item is EquipmentData)
            EquipData.Add(item as EquipmentData);
        else if (item is ConsumableData)
            consumData.Add(item as ConsumableData);
    }
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public List<ItemDataSO> itemSOList;
    public string saveFileName;
    public string saveFilePath => $"{Application.dataPath}/{saveFileName}.json";

    //��������
    [HideInInspector]
    public PlayerData playerDataTest;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        InventoryManager.Refresh();
    }
    public void SaveData()
    {
        FileData save = new FileData();
        foreach(ItemData item in InventoryManager.Items)
        {
            save.AddItem(item);
        }
        print(PlayerManager.Data.experience);
        save.PlayerData = PlayerManager.Data;
        File.WriteAllText(saveFilePath, JsonUtility.ToJson(save));
    }

    public void LoadData()
    {
        FileData save;
        try
        {
            string json = File.ReadAllText(saveFilePath);
            save = JsonUtility.FromJson<FileData>(json);
        }
        catch
        {
            InventoryManager.Refresh();
            return;
        }
        //AddRange : �������� �������� �ѹ��� �ֱ����� ���
        InventoryManager.Items.AddRange(save.EquipData);
        InventoryManager.Items.AddRange(save.consumData);

        // SO����Ʈ���� ���� id�� ã�� ������
        foreach (var item in InventoryManager.Items)
            item.Data = itemSOList.Find(x => x.tableId == item.tableID);

        PlayerManager.Data = save.PlayerData;
        InventoryManager.Refresh();
    }

}


