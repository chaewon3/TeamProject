using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TunnelCameraEvent : MonoBehaviour
{
    public CinemachineVirtualCamera EventCamera;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            EventCamera.Priority = 11;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            EventCamera.Priority = 9;
    }
}
