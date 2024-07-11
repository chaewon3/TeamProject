using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    float mouseSensitivity = 200f;
    float dirX;
    float dirY;

    public Transform player;
    public Transform lookTarget;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked; // 게임 매니저로 옮기기
        Cursor.visible = true;
    }

    private void Update()
    {
        //Rotate();
    }

    private void LateUpdate()
    {
        transform.position = player.position;
        transform.LookAt(lookTarget);
    }

    //void Rotate()
    //{
    //    dirX += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
    //    dirY -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

    //    dirY = Mathf.Clamp(dirY, -90f, 90f);
    //    transform.localRotation = Quaternion.Euler(dirY, dirX, 0f);
    //}
}

/*
  //Vector3 camera = player.position;

        //transform.position = new Vector3(camera.x, camera.y + 3f, camera.z - 2.42f);

        //transform.rotation = Quaternion.LookRotation(player.position, Vector3.up);
        //transform.LookAt(player);

        //transform.Translate()

        float dirx += Input.GetAxis("Mouse X") * 3f;
        float diry += Input.GetAxis("Mouse Y") * 3f;
        //transform.Rotate(0, dirx, 0, Space.World);
        transform.localEulerAngles = new Vector3(diry, dirx, 0f);
 */