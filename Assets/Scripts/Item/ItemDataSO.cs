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

//[System.Serializable]
//public class SerialItemData
//{
//    public ItemType type;
//    public int tableId;
//    public string name;
//    public int price;
//    public string description;
//    protected bool CanOverlap;
//}

// tableid ��� 1~10 �Һ� 101~110 ���� 201~210
public abstract class ItemDataSO : ScriptableObject
{
    public ItemType type;
    public int tableId;
    public string name;
    public int price;
    public Sprite ItemSpr;
    [TextArea(5, 10)]
    public string description;
    protected bool CanOverlap;
    //public void SetData(SerialItemData data)
    //{
    //    type = data.type;
    //
    //}
    //
    //public SerialItemData GetData()
    //{
    //    var @return = new SerialItemData();
    //    @return.type = type;
    //
    //    return @return;
    //}
    //
}



