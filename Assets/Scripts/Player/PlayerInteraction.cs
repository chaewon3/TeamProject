using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable interactable;
    private void Update()
    {
        if (interactable == null)
            return;

        if(Input.GetKeyDown(KeyCode.G))
        {
            interactable.interaction(true);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            interactable.interaction(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out IInteractable interact))
        {
            print("???");
            interactable = interact;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out IInteractable interact))
        {
            interactable = null;
        }
    }
}
