using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable interactable;
    Notification notion;

    private void Start()
    {
        notion = FindObjectOfType<Notification>();
    }
    private State state = State.Idle;
    enum State
    {
        Idle,
        Inventory
    }
    private void Update()
    {
        if (!GameManager.Instance.gamestart)
            return;
        if(Input.GetKeyDown(KeyCode.G) && state == State.Idle)
        {
            if (interactable == null)
                return;
            interactable.interaction(true);
            UISound.Instance.Inven();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(interactable != null)
            {
                interactable.interaction(false);
                UISound.Instance.Inven();
                return;
            }
            switch (state)
            {
                case State.Inventory: CanvasManager.ShowPlayer();
                    UISound.Instance.Inven();
                    state = State.Idle; break;
                default: CanvasManager.Exit();
                    break;
            }
            state = State.Idle;
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(state == State.Idle)
            {
                UISound.Instance.Inven();
                CanvasManager.ShowInentory();
                state = State.Inventory;
            }
            else if(state == State.Inventory)
            {
                UISound.Instance.Inven();
                CanvasManager.ShowPlayer();
                state = State.Idle;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IInteractable>(out IInteractable interact))
        {
            interactable = interact;
            if (other.gameObject.layer == LayerMask.NameToLayer("Interactable"))
                return;
            interact.interaction(true);
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
