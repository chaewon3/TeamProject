using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��� ���� ���ݴ� �����غ��� �� ��
[System.Flags]
public enum ItemType
{
    /// <summary>
    /// 0b�� ���ڴ� 10������ �����ϱ� ����
    /// �κ��丮 ���/���������� ���� �з�����
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


