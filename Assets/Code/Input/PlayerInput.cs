using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;

    private InputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new InputActions();
        playerInputActions.PlayerControl.Enable();
    }

    public Vector2 GetCharacterMovement()
    {
        return playerInputActions.PlayerControl.Movement.ReadValue<Vector2>();
    }
}
