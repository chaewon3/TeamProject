using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플래그 쓸지 조금더 생각해봐야 할 듯
[System.Flags]
public enum ItemType
{
    /// <summary>
    /// 0b는 숫자는 10진수와 구분하기 위함
    /// 인벤토리 토글/장착슬롯을 위해 분류했음
    /// </summary>
    Equipment_ARMOR     = 0b1,
    Equipment_SWORD     = 0b10,
    Equipment_BOW       = 0b100,

    Consumable          = 0b1000,
    Artifact            = 0b10000,
    Etc                 = 0b100000
}

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

public abstract class ItemData : ScriptableObject
{
    public ItemType type;
    public int id;
    public string name;
    public Sprite ItemSpr;
    [TextArea(5, 10)]
    public string description;
    public bool CanOverlap;
}

[CreateAssetMenu(fileName = "Item", menuName = "Add Item/Equipment")]
public class Equipment : ItemData
{
    public int DEF;
    public int ATK;
    public InchantType Inchant;
}

[CreateAssetMenu(fileName = "Item", menuName = "Add Item/Consumable")]
public class Consumable : ItemData
{
    public int count;
    public float coolTime;
    public effect effect;
}


