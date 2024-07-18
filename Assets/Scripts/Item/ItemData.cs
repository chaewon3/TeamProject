using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ �����ϸ� ���� ID�� ������ 
/// ���� �����͵��� ���尡���ϰ� ���� ������Ŭ����
/// </summary>
public abstract class ItemData : MonoBehaviour
{
    //����������
    public int UniqueID;

    //����������
    public ItemType type;
    public int tableId;
    public string name;
    public int price;
   // sprite�� ���� ���ϹǷ� ��� �ؾ����� ���� �ʿ�
    //public Sprite ItemSpr;
    [TextArea(5, 10)]
    public string description;
    protected bool CanOverlap;

    public void SetData(ItemDataSO SO)
    {
        type = SO.type;
        tableId = SO.tableId;
        name = SO.name;
        price = SO.price;
        description = SO.description;
    }
}

public class Equipment : ItemData
{
    public int DEF;
    public int ATK;
    public InchantType Inchant;
}

public class Consumable : ItemData
{
    //���� ������
    public int count;

    // ���� ������
    public float coolTime;
    public effect effect;

    public void SetData(ConsumableDataSO SO)
    {
        base.SetData(SO);
        coolTime = SO.coolTime;
        effect = SO.effect;
    }
}
