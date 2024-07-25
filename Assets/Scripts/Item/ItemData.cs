using UnityEngine;

/// <summary>
/// 플레이어가 습득하면 Scriptable Object의 정보를 받아와 
/// json 으로 저장가능하게 만든 데이터 클래스
/// </summary>
[System.Serializable]
public class ItemData
{
    protected ItemDataSO data;
    public ItemDataSO Data { get => data; set => data = value; }
    // id를 key값으로 ItemdataSO를 불러오기 위함
    public int tableID;
    public int UniqueID;

    public int amount;

    public ItemData(ItemDataSO data, int amount = 1)
    {
        this.data = data;
        tableID = data.tableId;
        this.amount = amount;
    }

    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }

}

[System.Serializable]
public class EquipmentData : ItemData
{
    public EquipmentDataSO DataEquip { get => data as EquipmentDataSO; }

    public int additionalAbility;
    public int[] upgrade = new int[3];

    /// <summary>
    /// 아이템을 첫 습득할 때 원본아이템 데이터를 복사해서 생성
    /// </summary>
    /// <param name="data">EquipmentDataSO가 아니면 오류남</param>
    public EquipmentData(EquipmentDataSO data) : base(data, 1)
    {
        if (data is not EquipmentDataSO)
            return;
    }

    /// <summary>
    /// 저장된 아이템 데이터를 불러올 때 호출 할 생성자 
    /// 첫번째 생성자와 부모의 생성자가 같이 호출됨.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="Inchant"></param>
    public EquipmentData(EquipmentDataSO data, int additionalAbility, int[] upgrade) : this(data)
    {
        this.additionalAbility = additionalAbility;
        this.upgrade = upgrade;
    }

    public void Upgrade(int enforce, int slotlevel)
    {
        upgrade[slotlevel] = enforce;
        additionalAbility += enforce;
    }
}

[System.Serializable]
public class ConsumableData : ItemData
{
    public int stack = 1;
    public ConsumableDataSO DataConsum { get => data as ConsumableDataSO; }

    public ConsumableData(ConsumableDataSO data, int amount = 1) : base(data, amount)
    {
        if (this.data.type == ItemType.Consumable)
        {
            stack = 5;
            this.amount = 5;
        }
    }

}
