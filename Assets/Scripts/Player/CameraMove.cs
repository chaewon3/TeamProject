using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    public Transform lookTarget;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked; // 게임 매니저로 옮기기
    }

    void LateUpdate()
    {
        transform.position = player.position;
        transform.LookAt(lookTarget);
    }

}
