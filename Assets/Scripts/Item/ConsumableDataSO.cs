using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Add Item/Consumable")]
public class ConsumableDataSO : ItemDataSO
{
    public float coolTime;
    public effect effect;

}
