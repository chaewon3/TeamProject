using UnityEngine;

public enum EquipType
{
    Armor,
    Weapon
}

[CreateAssetMenu(fileName = "Item", menuName = "Add Item/Equipment")]
public class EquipmentDataSO : ItemDataSO
{
    public EquipType equiptype;
}
