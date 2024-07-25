using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    AudioSource audioSource;
    public AudioClip[] clips;

    void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        DungeonBGM();
        audioSource.volume = 1f;
    }

    public void DungeonBGM()
    {
        audioSource.clip = clips[0];
        audioSource.Play();
    }

    public void BossBGM()
    {
        audioSource.clip = clips[1];
        audioSource.volume = 0.5f;
        audioSource.Play();
        
    }

    public void VolumeDown()
    {
        audioSource.volume = 0.3f;
    }


}
