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

// todo ���߿� �̸��� ������ �ٲ�� ��
public enum InchantType
{
    None,
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
// tableid ��� 1~10 �Һ� 101~110 ���� 201~210
public abstract class ItemDataSO : ScriptableObject
{
    public ItemType type;
    public int tableId;
    public string name;
    public int price;
    public Sprite icon;
    public GameObject ModelPrefab;
    [TextArea(5, 10)]
    public string description;
}



