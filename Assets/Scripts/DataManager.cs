using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [HideInInspector]
    public PlayerData playerData;
    public Dictionary<int, Inventory> dicInventory;

    void Awake()
    {
        if(Instance == null)
            Instance = this;

        playerData = PlayerFileLoad("PlayerInfo");

    }

    void Start()
    {
        
    }

    public void PlayerInfoSave(float maxHP, float dmg, float exp, int level)
    {
        string path = $"{Application.streamingAssetsPath}/PlayerInfoData.json";
        playerData.maxHP = maxHP;
        playerData.damage = dmg;
        playerData.experience = exp;
        playerData.level = level;
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(path, json);
    }

    //void Save()
    //{
    //    string path = $"{Application.streamingAssetsPath}/PlayerInfoData.json";
    //    string json = JsonUtility.ToJson(playerData);
    //    File.WriteAllText(path, json);
    //}

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
}

[System.Serializable]
public class Inventory
{
    public int[] equipSlots = new int[3];
    public int[] ArtifactsSlots = new int[3];
    //public List<Item> InvenSlots = new List<Item>();

}