using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    public Transform lookTarget;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked; // ���� �Ŵ����� �ű��
    }

    private void LateUpdate()
    {
        transform.position = player.position;
        transform.LookAt(lookTarget);
    }

}
