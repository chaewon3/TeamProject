using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveSound : MonoBehaviour
{
    public AudioClip walkingSound;
    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void BossWalking()
    {
        audioSource.PlayOneShot(walkingSound);
    }
}
