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
        // todo ĳ���Ϳ��� �Է� ��ȣ �ְ� ontrigger bool���� ���� ����ǰ� �����
    }

}
