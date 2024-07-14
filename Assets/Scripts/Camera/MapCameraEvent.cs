using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MapCameraEvent : MonoBehaviour
{
    public CinemachineVirtualCamera MapCam;

    bool onTrigger;
    private void Update()
    {
        if(onTrigger)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                GameManager.Instance.MouseLock(false);
                MapCam.Priority = 11;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.MouseLock(true);
                MapCam.Priority = 9;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        onTrigger = false;
    }

    private void OnTriggerStay(Collider other)
    {
        // todo 캐릭터에서 입력 신호 넣고 ontrigger bool값에 따라 실행되게 만들기
    }

}
