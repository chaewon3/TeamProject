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
    public InchantType Inchant;

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
    public EquipmentData(EquipmentDataSO data, int additionalAbility, InchantType Inchant) : this(data)
    {
        this.additionalAbility = additionalAbility;
        this.Inchant = Inchant;
    }

    //todo ������ �ö󰡴� �κ��̳� ��/���� �ٸ��� �Ұ� ���߿� �ٽ� �����
    public void Upgrade(InchantType type)
    {
        if(Inchant != InchantType.None)
        {
            return; // �̹� ��æƮ �� �Ӽ��� �ִٸ� ����
        }

        Inchant = type;
    }
}

[System.Serializable]
public class ConsumableData : ItemData
{
    public ConsumableDataSO DataConsum { get => data as ConsumableDataSO; }

    public ConsumableData(ConsumableDataSO data, int amount = 1) : base(data, amount)
    {
        //
    }

}
