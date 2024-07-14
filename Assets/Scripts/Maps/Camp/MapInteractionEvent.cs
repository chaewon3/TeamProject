using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteractionEvent : MonoBehaviour
{
    public void LoadGame(string scenename)
    {
        GameManager.Instance.LoadScene(scenename);
    }
}
