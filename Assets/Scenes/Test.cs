using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        print("안녕하세요f ~!<");
        print("네 안녕해요");
    }

    float a;

    private void Awake()
    {
        print("제일먼저 안녕하세요");

        this.a = (float)3d;
        int a = 0;
    }
}
