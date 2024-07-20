using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameobjectManager : MonoBehaviour
{
    public static PlayerGameobjectManager instance;

    public GameObject playerObject;

    private void Awake()
    {
        instance = this;

        playerObject = GameObject.FindWithTag("Player");

        if (playerObject != null)
        {
            //print(playerObject.name);
        }
    }

}
