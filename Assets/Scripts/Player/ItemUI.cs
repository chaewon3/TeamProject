using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    bool healPortion = true;
    public Image portion;
    float fillAmount = 1f;
    float totalTime = 10f;

    void Awake()
    {
        portion.fillAmount = 0f;
    }

    void Update()
    {
        if(healPortion && Input.GetKeyDown(KeyCode.E))
        {
            healPortion = false;
            portion.fillAmount = 1f;
            PlayerManager.Instance.Healing(10f);
        }   

        if(!healPortion && fillAmount > 0)
        {
            fillAmount = fillAmount - (Time.deltaTime / (totalTime - 1));
            portion.fillAmount = fillAmount;

            if (portion.fillAmount == 0)
            {
                healPortion = true;
                fillAmount = 1f;
            }
        }   
    } 
}
