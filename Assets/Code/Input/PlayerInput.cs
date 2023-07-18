using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

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

    public Vector2 GetMouseMovement()
    {
        return playerInputActions.PlayerControl.CameraMovement.ReadValue < Vector2>();
    }

}
