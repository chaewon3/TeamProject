using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapCameraEvent : MonoBehaviour
{
    public CinemachineVirtualCamera MapCam;
        

    private void OnTriggerStay(Collider other)
    {
        // todo 지도 볼때 캐릭터 움직임 제어, 마우스 중앙 풀기(게임매니저에서 관리)
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
