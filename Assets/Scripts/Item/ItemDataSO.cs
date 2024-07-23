using UnityEngine;

public enum ItemType
{
    Equipment_ARMOR     = 1 << 0,
    Equipment_SWORD     = 1 << 1,
    Equipment_BOW       = 1 << 2,

    Consumable          = 1 << 3,
    Artifact            = 1 << 4,
    Etc                 = 1 << 5
}


public enum effect
{
    None,
    Test2,
    Test3
}
// tableid 장비 1~10 소비 101~110 유물 201~210
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



