using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPointTest : MonoBehaviour
{

    Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit,100, 1<<LayerMask.NameToLayer("Default"))) {
            transform.position = hit.point;
        };

        
    }
}
