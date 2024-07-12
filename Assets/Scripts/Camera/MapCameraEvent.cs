using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapCameraEvent : MonoBehaviour
{
    public CinemachineVirtualCamera MapCam;
        

    private void OnTriggerStay(Collider other)
    {
        // todo ���� ���� ĳ���� ������ ����, ���콺 �߾� Ǯ��(���ӸŴ������� ����)
        if (Input.GetKeyDown(KeyCode.G))
        {
            MapCam.Priority = 11;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MapCam.Priority = 9;
        }
    }

}
