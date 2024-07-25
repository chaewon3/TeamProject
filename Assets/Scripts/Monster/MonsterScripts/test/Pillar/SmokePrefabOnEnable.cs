using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokePrefabOnEnable : MonoBehaviour
{
    public AudioClip[] bumpSound;
    AudioSource audioSource;

    private void OnEnable()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        StartSound();
    }

    void StartSound()
    {
        int randValue = Random.Range(0,bumpSound.Length);


        audioSource.PlayOneShot(bumpSound[randValue]);

    }
}
