using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

    }

    public Vector2 GetMovementNormalize()
    {
        Vector2 input = playerInputActions.Movement.WASD.ReadValue<Vector2>();

        return input;
    }
}
