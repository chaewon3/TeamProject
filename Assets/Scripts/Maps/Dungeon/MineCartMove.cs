using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class MineCartMove : MonoBehaviour, IInteractable
{
    public GameObject MineCart;
    public GameObject MineCartCollapse;
    public Transform lever;
    public CinemachineVirtualCamera cutScenecam;
    bool isTrigger;
    float speed = 5f;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// ±¤Â÷ ¾Ö´Ï¸ÞÀÌ¼Ç°ú Virtual Camera ÀÌº¥Æ® ÄÆ¾À Á¦¾î
    /// </summary>
    IEnumerator LeverOn()
    {
        cutScenecam.Priority = 11;
        yield return new WaitForSeconds(2f);
        float time = 0;
        audioSource.Play();
        while (time <= 2.5f)
        {
            if (time >= 0.5f)
            {
                cutScenecam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().enabled = false;
            }
            MineCart.transform.Translate(-MineCart.transform.forward * speed * Time.deltaTime);
            time += Time.deltaTime;
            yield return null;
        }
        
        cutScenecam.enabled = false;
        audioSource.Stop();
        MineCartCollapse.SetActive(true);
        Destroy(MineCart, 0);
    }

    public void interaction(bool OnOff)
    {
        if (!isTrigger && OnOff)
        {
            lever.Rotate(-104, 0, 0);
            StartCoroutine(LeverOn());
            isTrigger = true;
        }
    }
}
