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

    //todo ������ �ö󰡴� �κ��̳� ��/���� �ٸ��� �Ұ� ���߿� �ٽ� �����
    public bool Upgrade(int enforce, int skillPoint)
    {
        int cost = 1;
        for(int i = 0; i<3;i++)
        {
            if (upgrade[i] != 0 && cost <= skillPoint)
            {
                upgrade[i] = enforce;
                additionalAbility += enforce;
                // todo needcost��ŭ ���� ��ų����Ʈ���� �����°� ������
                // playerdata.skillpoint -= cost;
                return true;
            }
            cost++;
        }
        return false;
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
