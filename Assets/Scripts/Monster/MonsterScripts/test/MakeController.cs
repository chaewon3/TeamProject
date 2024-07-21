using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeController : MonoBehaviour
{

    CharacterController charCont;
    float moveSpeed = 3;

    void Awake()
    {
        charCont = GetComponent<CharacterController>();
    }

    void Update()
    {
        #region ¿Ãµø

        Vector3 moveDir = Vector3.zero;
        moveDir.z = Input.GetAxisRaw("Vertical");
        moveDir.x = Input.GetAxisRaw("Horizontal");

        charCont.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
        charCont.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);

        #endregion
    }
}
