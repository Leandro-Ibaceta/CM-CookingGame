using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;

    PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Movement.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if(OnInteractAction != null)
        {
            OnInteractAction(this, EventArgs.Empty);
        }
    }

    public Vector2 GetMovementNormalize()
    {
        Vector2 input = playerInputActions.Movement.WASD.ReadValue<Vector2>();

        return input;
    }
}
