using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    AudioSource playerAudio;
    Animator ani;
    public AudioClip[] clip;

    void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
    }

    public void FootRightSound()
    {
        playerAudio.PlayOneShot(clip[0], 0.2f);
    }

    public void FootLeftSound()
    {
        playerAudio.PlayOneShot(clip[1], 0.2f);
    }

    public void SwordCombo01Sound()
    {
        playerAudio.PlayOneShot(clip[2], 0.2f);
    }

    public void SwordCombo02Sound()
    {
        playerAudio.PlayOneShot(clip[3], 0.2f);
    }

    public void SwordCombo03Sound()
    {
        playerAudio.PlayOneShot(clip[4], 0.2f);
    }

    public void ArrowSound()
    {
        playerAudio.PlayOneShot(clip[5], 0.2f);
    }
}
