using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        print("�ȳ��ϼ���f ~!<");
        print("�� �ȳ��ؿ�");
    }

    float a;

    private void Awake()
    {
        print("���ϸ��� �ȳ��ϼ���");

        this.a = (float)3d;
        int a = 0;
    }
}
