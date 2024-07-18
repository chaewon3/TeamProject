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


    public void PlayerInfoSave(float maxHP, float dmg, float exp, int level, int arrow) //todo 이거 배열로 파라미터 변경 할건지?
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
    // 인벤토리나 장비 슬롯 등 가지고 있는 아이템은 ID로만 구별
    public int[] equipSlots = new int[3];
    public int[] ArtifactsSlots = new int[3];
    public List<int> InvenSlots = new List<int>();
    // 인벤토리 List에 들어있는 아이템 ID를 키워드로 저장해 둔 고유 ItemData를 불러옴
    public Dictionary<int, ItemData> Items = new Dictionary<int, ItemData>();

}