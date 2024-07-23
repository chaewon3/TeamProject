using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerData
{
    public float maxHealth = 100f;
    public float damage = 10f;
    public float experience = 0f;
    public int level = 1;
    public int ArrowCount = 12;
    public int skillPoint = 5;
    public int Gem = 0;
}

public class PlayerManager : MonoBehaviour
{
    #region 전역 변수 
    public static PlayerManager Instance;

    PlayerData playerData = new PlayerData();

    [HideInInspector]
    public int ArrowCount;

    public static PlayerData Data { get => Instance.playerData;  set => Instance.playerData = value; }
    #endregion

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
}


