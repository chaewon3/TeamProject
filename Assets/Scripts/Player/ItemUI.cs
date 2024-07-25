using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    PlayerInfo player;
    bool healPortion = true;
    public Image portion;
    float fillAmount = 1f;
    float totalTime = 10f;
    AudioSource portionAudio;

    void Awake()
    {
        portion.fillAmount = 0f;
        player = FindObjectOfType<PlayerInfo>();
        portionAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(healPortion && Input.GetKeyDown(KeyCode.E))
        {
            healPortion = false;
            portion.fillAmount = 1f;
            player.Healing(10f);
            portionAudio.Play();
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
