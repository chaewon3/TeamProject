using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    AudioSource audioSource;
    bool dungeonClear = false;
    public GameObject stageBoss;
    public GameObject potal;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {   
        if(!dungeonClear && !stageBoss.activeSelf)
        {
            SoundManager.Instance.DungeonBGM();

            audioSource.Play();
            dungeonClear = true;
            potal.SetActive(true);
        }
    }

}
