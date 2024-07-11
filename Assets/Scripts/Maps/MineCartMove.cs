using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCartMove : MonoBehaviour
{
    public GameObject MineCart;
    public Transform lever;
    bool isTrigger;
    float speed = 5f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G) && !isTrigger)
        {
            lever.Rotate(-104, 0, 0);
            StartCoroutine(LeverOn());
            isTrigger = true;
        }
    }
    IEnumerator LeverOn()
    {        
        float time = 0;
        while (time <= 3)
        {
            MineCart.transform.Translate(-MineCart.transform.forward * speed * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(MineCart, 0);
    }
}
