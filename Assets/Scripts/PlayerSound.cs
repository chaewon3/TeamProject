using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    AudioSource audio;
    Animator ani;
    public AudioClip[] clip;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
    }

    public void FootRightSound()
    {
        audio.PlayOneShot(clip[0], 0.2f);
    }

    public void FootLeftSound()
    {
        audio.PlayOneShot(clip[1], 0.2f);
    }

    public void SwordCombo01Sound()
    {
        audio.PlayOneShot(clip[2], 0.2f);
    }

    public void SwordCombo02Sound()
    {
        audio.PlayOneShot(clip[3], 0.2f);
    }

    public void SwordCombo03Sound()
    {
        audio.PlayOneShot(clip[4], 0.2f);
    }

    public void ArrowSound()
    {
        audio.PlayOneShot(clip[5], 0.2f);
    }
}
