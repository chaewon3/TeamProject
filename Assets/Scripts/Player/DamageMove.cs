using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMove : MonoBehaviour
{
    Coroutine moveCoroutine;
    Vector3 startPos;
    Vector3 endPos;
    float duration = 3f;
    float elapsedTime = 0.0f;
    Transform target;

    void Awake()
    {
        startPos = new Vector3(-1f, 1.5f, 1f);
        endPos = new Vector3(-1f, 1.7f, 1f);
        target = GameObject.Find("Main Cam").transform;
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
            Vector3 dire = target.position - transform.position;
            dire.y = 0;
            if(dire.sqrMagnitude > 0.01f)
            {
                Quaternion targetRota = Quaternion.LookRotation(dire);
                transform.rotation = targetRota * Quaternion.Euler(0, 180, 0);
            }

            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float easedT = EaseOutBack(t);
            transform.localPosition = Vector3.Lerp(startPos, endPos, easedT);
            yield return null;
        }
        transform.localPosition = endPos;
    }
}
