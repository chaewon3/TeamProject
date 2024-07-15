using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public Item itemdatas;
    private Dictionary<int, Item> dicItemDatas;
    private void Awake()
    {
        
    }
}

[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public string nameSpr;
    public string desc;
    public int price;
    public int classification;
}

public class Equipment : Item
{
    public int DEF;
    public int ATK;
    public int Inchant;
}

public class Artipacts : Item
{
    public int count;
}

public class Expendables : Item
{
    public int count;
    public int efficacy;
}
