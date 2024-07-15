using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineRender : MonoBehaviour
{
    public MeshRenderer[] interactionrenderer;
    public Material outlineMAT;

    private void OnTriggerEnter(Collider other)
    {
        foreach(MeshRenderer renderer in interactionrenderer)
        {
            Material[] tempMAT = renderer.materials;
            tempMAT[1] = outlineMAT;
            renderer.materials = tempMAT;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (MeshRenderer renderer in interactionrenderer)
        {
            Material[] tempMAT = renderer.materials;
            tempMAT[1] = null;
            renderer.materials = tempMAT;
        }
    }

}
