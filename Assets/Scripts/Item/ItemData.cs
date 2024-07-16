using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment_ARMOR     = 0b1,
    Equipment_SWORD     = 0b10,
    Equipment_BOW       = 0b100,

    Consumable          = 0b1000,
    Artifact            = 0b10000,
    Etc                 = 0b100000
}

// todo 나중에 이름과 종류는 바꿔야 함
public enum InchantType
{
    Test1,
    Test2,
    Test3
}

public enum effect
{
    Test1,
    Test2,
    Test3
}

// id 장비 1~10 소비 101~110 유물 201~210
public abstract class ItemData : ScriptableObject
{
    public ItemType type;
    public int tableId;
    public string name;
    public int price;
    public Sprite ItemSpr;
    [TextArea(5, 10)]
    public string description;
    protected bool CanOverlap;
}

[CreateAssetMenu(fileName = "Item", menuName = "Add Item/Equipment")]
public class Equipment : ItemData
{
    public int DEF;
    public int ATK;
    public InchantType[] Inchant = new InchantType[3];
    private void Awake()
    {
        CanOverlap = false;
    }
}

[CreateAssetMenu(fileName = "Item", menuName = "Add Item/Consumable")]
public class Consumable : ItemData
{
    public int count;
    public float coolTime;
    public effect effect;
    private void Awake()
    {
        CanOverlap = true;
    }
}


