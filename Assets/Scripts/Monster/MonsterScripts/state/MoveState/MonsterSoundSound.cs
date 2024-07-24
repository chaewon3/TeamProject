using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoundSound : MonoBehaviour
{
    public AudioClip walkingSound;
    public AudioClip[] attackSound;
    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void WalkingSound()
    {
        audioSource.PlayOneShot(walkingSound);
    }

    void AttackSound()
    {
        int randValue = Random.Range(0, attackSound.Length);


        audioSource.PlayOneShot(attackSound[randValue]);
    }
}
