using UnityEngine;

/// <summary>
/// �÷��̾ �����ϸ� Scriptable Object�� ������ �޾ƿ� 
/// json ���� ���尡���ϰ� ���� ������ Ŭ����
/// </summary>
[System.Serializable]
public class ItemData
{
    protected ItemDataSO data;
    public ItemDataSO Data { get => data; set => data = value; }
    // id�� key������ ItemdataSO�� �ҷ����� ����
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
    /// �������� ù ������ �� ���������� �����͸� �����ؼ� ����
    /// </summary>
    /// <param name="data">EquipmentDataSO�� �ƴϸ� ������</param>
    public EquipmentData(EquipmentDataSO data) : base(data, 1)
    {
        if (data is not EquipmentDataSO)
            return;
    }

    /// <summary>
    /// ����� ������ �����͸� �ҷ��� �� ȣ�� �� ������ 
    /// ù��° �����ڿ� �θ��� �����ڰ� ���� ȣ���.
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
