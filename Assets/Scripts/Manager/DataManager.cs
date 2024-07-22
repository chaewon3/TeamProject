using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileData
{
    //public List<PlayerData> playerData;
    public List<EquipmentData> EquipData;
    public List<ConsumableData> consumData;

    public FileData()
    {
        //playerData = new List<PlayerData>();
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

        playerDataTest = PlayerFileLoad("PlayerInfo");
        LoadData();
    }


    public void PlayerInfoSave(float maxHP, float dmg, float exp, int level, int arrow) //todo �̰� �迭�� �Ķ���� ���� �Ұ���?
    {
        string path = $"{Application.streamingAssetsPath}/PlayerInfoData.json";
        playerDataTest.maxHP = maxHP;
        playerDataTest.damage = dmg;
        playerDataTest.experience = exp;
        playerDataTest.level = level;
        playerDataTest.ArrowCount = arrow;
        string json = JsonUtility.ToJson(playerDataTest);
        File.WriteAllText(path, json);
    }

    
    public void SaveData()
    {
        FileData save = new FileData();
        foreach(ItemData item in InventoryManager.Items)
        {
            save.AddItem(item);
        }
        // todo PlayerData �����ϴ� �κе� �־�� ��

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

        InventoryManager.Refresh();
    }


    public PlayerData PlayerFileLoad(string fileName)
    {
        DirectoryInfo di = new DirectoryInfo(Application.streamingAssetsPath);

        foreach(FileInfo file in di.GetFiles())
        {
            if(file.Name == $"{fileName}Data.json")
            {
                string path = $"{Application.streamingAssetsPath}/PlayerInfoData.json";

                if (File.Exists(path) && file.Extension == ".json")
                {
                    string json = File.ReadAllText(path);
                    return JsonUtility.FromJson<PlayerData>(json);
                }
            }
        }
        return null;
    }


    //void firstDataSave()
    //{
    //    foreach(PlayerData data in playerData)
    //    {
    //        string path = $"{Application.streamingAssetsPath}/PlayerInfoData.json";
    //        string json = JsonUtility.ToJson(data);
    //        File.WriteAllText(path, json);
    //    }
    //}
}

[System.Serializable]
public class PlayerData
{
    public float maxHP;
    public float damage;
    public float experience;
    public int level;
    public int ArrowCount;
}

