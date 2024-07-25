using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class PlayerData
{
    public float maxHealth = 100f;
    public float meleeDamage = 10f;
    public float longDamage = 5f;
    public float experience = 0f;
    public int level = 1;
    public int ArrowCount = 12;
    public int skillPoint = 5;
    public int Gem = 0;
}

public class PlayerManager : MonoBehaviour
{
    #region ���� ���� 
    public static PlayerManager Instance;

    PlayerData playerData = new PlayerData();
    PlayerInfo playerinfo;

    [HideInInspector]
    public int ArrowCount;

    public static PlayerData Data { get => Instance.playerData;  set => Instance.playerData = value; }

    public GameObject[] EquipArray = new GameObject[3];
    public GameObject[] Equips = new GameObject[6];

    #endregion

    void Awake()
    {
        Instance = this;
    } 

    public void EquipChange(int Itype, int id)
    { 
        for (int i = 0; i < EquipArray[Itype].transform.childCount; i++)
        {
            EquipArray[Itype].transform.GetChild(i).gameObject.SetActive(false);
        }
        Equips[id-1].SetActive(true);
    }

    public void TakeOFF(int id)
    {
        Equips[id-1].SetActive(false);
    }

    public  void ExperienceUp(float value)
    {
        Data.experience += value;
    }

    public void EquipAddStats(int slot, int value)
    {
        switch(slot)
        {
            case 0:
                Data.meleeDamage += value;
                break;
            case 1:
                Data.maxHealth += value;
                playerinfo = FindObjectOfType<PlayerInfo>();
                playerinfo.currentHealth += value;
                break;
            case 2:
                Data.longDamage += value;
                break;
        }
    }

    public void EquipSubStats(int slot, int value)
    {
        switch (slot)
        {
            case 0:
                Data.meleeDamage -= value;
                break;
            case 1:
                Data.maxHealth -= value;
                playerinfo = FindObjectOfType<PlayerInfo>();
                playerinfo.currentHealth -= value;
                break;
            case 2:
                Data.longDamage -= value;
                break;
        }
    }
}



// , ui����, ����ġ, ������, ����, ���� ui, ���� Ż��