using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : NewBehaviourScript
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<NoBehaviourScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
