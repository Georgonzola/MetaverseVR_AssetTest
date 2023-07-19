using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{

    private InputActions playerInputActions;

    private bool toggleLock = true;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        playerInputActions = new InputActions();
        playerInputActions.PlayerControl.Enable();
        playerInputActions.PlayerControl.CursorLock.performed += Lock_Cursor;
    }

    private void Lock_Cursor(InputAction.CallbackContext context)
    {
        //toggles cursor lock state, does not allow cursor lock on menu as this prevents the button press
        if (toggleLock && SceneManager.GetActiveScene().buildIndex!=2)
        {
            Cursor.lockState = CursorLockMode.Locked;
            toggleLock = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            toggleLock = true;
        }
    }

    public Vector2 GetCharacterMovement()
    {
        return playerInputActions.PlayerControl.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseMovement()
    {
        return playerInputActions.PlayerControl.CameraMovement.ReadValue <Vector2>();
    }


}
