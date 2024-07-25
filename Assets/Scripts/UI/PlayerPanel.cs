using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    public EquipSlot[] ArtifactSlot = new EquipSlot[3];

    public void Referesh(ItemData[] Artifact)
    {
        for(int i = 0;i<3;i++)
        {
            if (Artifact[i] == null || Artifact[i].tableID == 0)
                ArtifactSlot[i].Clear();
            else
            {
                ArtifactSlot[i].setItem(Artifact[i]);
            }
        }
    }
}
