using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageMove : MonoBehaviour
{
    Coroutine moveCoroutine;
    Vector3 startPos;
    Vector3 endPos;
    float duration = 3f;
    float elapsedTime = 0.0f;

    void Awake()
    {
        startPos = new Vector3(0f, 0.5f, -0.5f);
        endPos = new Vector3(0f, 0.7f, -0.5f);
    }

    void OnEnable()
    {
        transform.position = startPos;
        moveCoroutine = StartCoroutine(DamageAnimation());
    }

    void OnDisable()
    {
        transform.position = startPos;
    }

    float EaseOutBack(float x)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;

        return 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2);
    }

    IEnumerator DamageAnimation()
    {
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float easedT = EaseOutBack(t);
            transform.localPosition = Vector3.Lerp(startPos, endPos, easedT);
            yield return null;
        }
        transform.localPosition = endPos;
    }
}
