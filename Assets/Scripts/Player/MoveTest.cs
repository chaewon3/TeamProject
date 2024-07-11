 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    float dirSpeed = 3f;
    float moveSpeed = 3f;
    float rotateDegree;
    CharacterController charCont;
    Vector3 playerPos;

    private void Awake()
    {
        charCont = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out var hit, 100, 1 << LayerMask.NameToLayer("Default")))
        {
            playerPos = hit.point;
        };

        transform.LookAt(playerPos);
    }        
}

/*
 //if (Input.GetMouseButton(1))
        //{
        //    transform.Rotate(0f, Input.GetAxis("Mouse X") * dirSpeed, 0f, Space.World);
        //}

        //float z = Input.GetAxisRaw("Vertical");
        //Vector3 Direction = Vector3.forward * z;

        //charCont.Move(Direction * moveSpeed * Time.deltaTime);


        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = transform.position;

        mousePos.z = playerPos.z - Camera.main.transform.position.z;

        Vector3 target = Camera.main.ScreenToWorldPoint(mousePos);

        //float dirX = target.x - playerPos.x;
        //float dirZ = target.z - playerPos.z;

        //if (target.z > playerPos.z)
        //{
        //    print("플레이어 위");
        //    rotateDegree = Mathf.Atan2(dirX, dirZ) * Mathf.Rad2Deg - 180;
        //}
        //else
        //{
        //    print("플레이어 아래");
        //    rotateDegree = Mathf.Atan2(dirX, dirZ) * Mathf.Rad2Deg;
        //}

        //transform.rotation = Quaternion.Euler(0f, rotateDegree, 0f);

        target.y = 0;
        transform.LookAt(target);



        if(Input.GetMouseButtonDown(0))
        {
            //print($"dirX: {dirX}");
            ////print($"playerPos.x: {playerPos.x}");
            //print($"dirZ: {dirZ}");
            print($"playerPos.z: {playerPos.z}");
            //print(mousePos.x);
            //print(playerPos.x);
        }

        //Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //moveDir = moveDir * moveSpeed * Time.deltaTime;

        //if(moveDir != Vector3.zero)
        //{
        //    gameObject.transform.forward = moveDir;
        //}

        //charCont.Move(moveDir);




        //float x = Input.GetAxisRaw("Horizontal");
        //float z = Input.GetAxisRaw("Vertical");

        //Vector3 Direction = new Vector3(x, 0, z);
        //charCont.Move(Direction * moveSpeed * Time.deltaTime);



        //if (Input.GetMouseButton(1))
        //{
        //    Ray camerRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

        //    float rayLength;

        //    if (GroupPlane.Raycast(camerRay, out rayLength))
        //    {
        //        Vector3 pointTolook = camerRay.GetPoint(rayLength);
        //        transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        //    }
        //}
 */
