using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [HideInInspector]
    public PlayerData playerData;
    public Inventory inventoryData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        playerData = PlayerFileLoad("PlayerInfo");
        inventoryData = InventoryFileLoad("inventoryData");
    }


    public void PlayerInfoSave(float maxHP, float dmg, float exp, int level, int arrow) //todo �̰� �迭�� �Ķ���� ���� �Ұ���?
    {
        string path = $"{Application.streamingAssetsPath}/PlayerInfoData.json";
        playerData.maxHP = maxHP;
        playerData.damage = dmg;
        playerData.experience = exp;
        playerData.level = level;
        playerData.ArrowCount = arrow;
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(path, json);
    }

    
    public void InventorySave()
    {
        string path = $"{Application.streamingAssetsPath}/inventoryData.json";
        string json = JsonUtility.ToJson(inventoryData);
        File.WriteAllText(path, json);
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

    public Inventory InventoryFileLoad(string fileName)
    {
        DirectoryInfo di = new DirectoryInfo(Application.streamingAssetsPath);

        foreach(FileInfo file in di.GetFiles())
        {
            if(file.Name == $"{fileName}Data.Json")
            {
                string path = $"{Application.streamingAssetsPath}/{fileName}Data.json";

                if(File.Exists(path) && file.Extension == ".json")
                {
                    string json = File.ReadAllText(path);
                    return JsonUtility.FromJson<Inventory>(json);
                }
            }
        }
        return new Inventory();
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

[System.Serializable]
public class Inventory
{
    // �κ��丮�� ��� ���� �� ������ �ִ� �������� ID�θ� ����
    public int[] equipSlots = new int[3];
    public int[] ArtifactsSlots = new int[3];
    public List<int> InvenSlots = new List<int>();
    // �κ��丮 List�� ����ִ� ������ ID�� Ű����� ������ �� ���� ItemData�� �ҷ���
    public Dictionary<int, ItemData> Items = new Dictionary<int, ItemData>();

}