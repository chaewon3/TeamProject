using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    public Transform lookTarget;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked; // ���� �Ŵ����� �ű��
    }

    void LateUpdate()
    {
        transform.position = player.position;
        transform.LookAt(lookTarget);
    }

}
