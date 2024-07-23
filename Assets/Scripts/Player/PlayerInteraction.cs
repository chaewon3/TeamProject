using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable interactable;

    private State state = State.Idle;
    enum State
    {
        Idle,
        Map,
        Inventory
    }
    private void Update()
    {      
        if(Input.GetKeyDown(KeyCode.G) && state == State.Idle)
        {
            if (interactable == null)
                return;
            interactable.interaction(true);
            state = State.Map;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch(state)
            {
                case State.Map: interactable.interaction(false); break;
                case State.Inventory: CanvasManager.ShowPlayer(); break;
            }
            state = State.Idle;
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(state == State.Idle)
            {
                CanvasManager.ShowInentory();
                state = State.Inventory;
            }
            else if(state == State.Inventory)
            {
                CanvasManager.ShowPlayer();
                state = State.Idle;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<ItemObject>(out ItemObject item))
        {
            item.interaction(true);
            return;
        }
        if (other.TryGetComponent<IInteractable>(out IInteractable interact))
        {
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
