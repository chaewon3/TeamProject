using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPanel : MonoBehaviour
{
    CanvasGroup canvas;
    Coroutine panel;
    bool returnTown = false;
    AudioSource audioSource;

    void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        if(panel == null)
            panel = StartCoroutine(OnPanel());

        returnTown = false;
    }

    void Update()
    {
        if(returnTown && Input.anyKeyDown)
        {
            GameManager.Instance.GameOver("MainScene");
        }

    }

    IEnumerator OnPanel()
    {
        audioSource.Play();
        canvas.alpha = 0;
        float starttime = 0;
        while (starttime < 0.5f)
        {
            float t = (Time.time - starttime) / 0.5f;
            canvas.alpha = Mathf.Lerp(0, 1, starttime / 0.5f);
            starttime += Time.deltaTime;
            yield return null;
        }
        canvas.alpha = 1;
        yield return new WaitForSeconds(2f);
        returnTown = true;
        panel = null;
    }
}
