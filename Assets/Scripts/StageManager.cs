using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    bool dungeonClear = false;
    public GameObject stageBoss;
    public GameObject potal;

    void Update()
    {
        print(stageBoss.activeSelf);
        if(!dungeonClear && !stageBoss.activeSelf)
        {
            dungeonClear = true;
            potal.SetActive(true);
        }
    }

    //IEnumerator DunGeonClear()
    //{
    //    print("코루틴 들어옴");
    //    yield return new WaitForSeconds(1f);
    //    potal.SetActive(true);
    //}
}
