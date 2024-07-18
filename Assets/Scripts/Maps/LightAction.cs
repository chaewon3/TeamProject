using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAction : MonoBehaviour
{
    public Light Light;

    private void Start()
    {
        StartCoroutine(LightKindle());
    }

    IEnumerator LightKindle()
    {
        float time = 0;
        float starttime;
        while (true)
        {
            starttime = Time.time;
            while (time <= 1)
            {
                float t = (Time.time - starttime);
                Light.intensity = Mathf.Lerp(1.5f, 2.3f, t);
                time += Time.deltaTime;
                yield return null;
            }
            time = 0;
            starttime = Time.time;
            while (time <= 1)
            {
                float t = (Time.time - starttime);
                Light.intensity = Mathf.Lerp(2.3f, 1.5f, t);
                time += Time.deltaTime;
                yield return null;
            }
            time = 0;
            yield return null;
        }
    }
}
