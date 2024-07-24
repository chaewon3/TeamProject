using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour, IInteractable
{
    public void interaction(bool OnOff)
    {
        GameManager.Instance.LoadScene("MainScene");
    }
}
