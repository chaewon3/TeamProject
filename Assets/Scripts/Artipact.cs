using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artipact : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetArtipact(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ConsumableData Item = (ConsumableData)InventoryManager.Artifact[1];
            ArtipactEffect(Item.DataConsum.effect);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ConsumableData Item = (ConsumableData)InventoryManager.Artifact[2];
            ArtipactEffect(Item.DataConsum.effect);
        }
    }


    void GetArtipact(int index)
    {
        ConsumableData Item = (ConsumableData)InventoryManager.Artifact[index];
        ArtipactEffect(Item.DataConsum.effect);
    }


    void ArtipactEffect(effect index)
    {
        //��� ��Ƽ��Ʈ �Լ� ?
    }
}





