using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artipact : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ConsumableData Item = (ConsumableData)InventoryManager.Artifact[0];

            if (Item != null)
            {
                Instantiate(Item.DataConsum.usePrefab, player);
                InventoryManager.UseItem(Item, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ConsumableData Item = (ConsumableData)InventoryManager.Artifact[1];

            if (Item != null)
            {
                Instantiate(Item.DataConsum.usePrefab, player);
                InventoryManager.UseItem(Item, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ConsumableData Item = (ConsumableData)InventoryManager.Artifact[2];

            if (Item != null)
            {
                Instantiate(Item.DataConsum.usePrefab, player);
                InventoryManager.UseItem(Item, 0);
            }
        }
    }
}





