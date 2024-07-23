using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    bool healPortion = true;
    public Image portion;
    float fillAmount = 1f;
    float totalTime = 3f;

    void Awake()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            healPortion = false;

        if(healPortion && fillAmount > 0)
        {
            //healPortion = false;
            fillAmount = fillAmount - (Time.deltaTime / (totalTime - 1));
            portion.fillAmount = fillAmount;
        }
    }

    IEnumerator PortionCoolTime()
    {
        yield return new WaitForSeconds(2f);
    }    
}
