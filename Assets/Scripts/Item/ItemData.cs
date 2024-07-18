using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 습득하면 고유 ID를 가지며 
/// 가변 데이터들을 저장가능하게 만든 데이터클래스
/// </summary>
public abstract class ItemData : MonoBehaviour
{
    //가변데이터
    public int UniqueID;

    //원본데이터
    public ItemType type;
    public int tableId;
    public string name;
    public int price;
   // sprite는 저장 못하므로 어떻게 해야할지 생각 필요
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
    //가변 데이터
    public int count;

    // 원본 데이터
    public float coolTime;
    public effect effect;

    public void SetData(ConsumableDataSO SO)
    {
        base.SetData(SO);
        coolTime = SO.coolTime;
        effect = SO.effect;
    }
}
