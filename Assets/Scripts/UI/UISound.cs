using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour
{
    public AudioClip[] sounds;
    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Hovering()
    {
        audio.PlayOneShot(sounds[0], 0.15f);
    }

    public void Notion()
    {
        audio.PlayOneShot(sounds[1], 0.4f);
    }

    public void Click()
    {
        audio.PlayOneShot(sounds[2], 0.4f);
    }

    public void Equip()
    {
        audio.PlayOneShot(sounds[3], 0.4f);
    }

}
