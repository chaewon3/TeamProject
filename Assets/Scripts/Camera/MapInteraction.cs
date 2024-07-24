using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapInteraction : MonoBehaviour, IInteractable
{
    public CinemachineVirtualCamera MapCam;

    public void interaction(bool OnOff)
    {
        if(OnOff)
        {
            CanvasManager.ShowMap();
            MapCam.Priority = 11;
        }
        else
        {
            CanvasManager.ShowPlayer();
            MapCam.Priority = 9;
        }
    }
}
