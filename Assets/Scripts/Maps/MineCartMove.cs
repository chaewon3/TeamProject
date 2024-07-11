using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCartMove : MonoBehaviour
{
    public Transform MineCart;
    public Transform lever;
    bool isTrigger;

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
        float speed = 4f;
        
        
        speed = 3f;
        float time = 0;
        while (time <= 3)
        {
            MineCart.Translate(MineCart.transform.forward * speed * Time.deltaTime, Space.Self);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
