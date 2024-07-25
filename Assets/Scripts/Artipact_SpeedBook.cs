using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artipact_SpeedBook : MonoBehaviour
{
    public GameObject particle;
    PlayerMove playerMove;
    AudioSource audioSource;

    void Awake()
    {
        playerMove = FindObjectOfType<PlayerMove>();
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator Start()
    {
        audioSource.Play();
        playerMove.moveSpeed += 2f;
        yield return new WaitForSeconds(8f);
        
        playerMove.moveSpeed -= 2f;
        yield return new WaitForEndOfFrame();

        Destroy(gameObject);
    }
}
