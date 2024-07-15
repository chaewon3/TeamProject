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
            GameManager.Instance.MouseLock(false);
            MapCam.Priority = 11;
        }
        else
        {
            GameManager.Instance.MouseLock(true);
            MapCam.Priority = 9;
        }
    }
}
