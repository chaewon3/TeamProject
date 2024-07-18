using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Add Item/Equipment")]
public class EquipmentDataSO : ItemDataSO
{
    private void Awake()
    {
        CanOverlap = false;
    }
}
