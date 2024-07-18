using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITemList : MonoBehaviour
{
    public static ITemList instance;

    public List<ItemDataSO> ItemList;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
