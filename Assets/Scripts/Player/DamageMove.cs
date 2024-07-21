using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMove : MonoBehaviour
{
    Coroutine mainCoroutine;
    Coroutine moveCoroutine;

    void Start()
    {
        mainCoroutine = StartCoroutine(MainCoroutine());

    }

    IEnumerator MainCoroutine()
    {
        while (true)
        {
            moveCoroutine = StartCoroutine(MoveCoroutine());
            yield return moveCoroutine;
        }
    }

    IEnumerator MoveCoroutine()
    {
        float endTime = Time.time + 5;

        while (Time.time < endTime)
        {
            transform.Translate(new Vector3(0, 1f * Time.deltaTime, 0));
            yield return null;
        }
    }
}
