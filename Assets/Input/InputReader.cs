using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputActions;

[CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObjects/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    private PlayerInputActions inputActions;

    public event Action<bool> TouchEvent;
    public Vector2 TouchPosition { get; private set; }
    public Vector2 AimDirection { get; private set; }

    private void OnEnable()
    {
        if(inputActions == null)
        {
            inputActions = new PlayerInputActions();
            inputActions.Player.SetCallbacks(this);
        }

        AimDirection = Vector2.zero;

        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }


    public void OnTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch");
        if (context.performed)
        {
            TouchEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            TouchEvent?.Invoke(false);
        }
    }

    public void OnTouchPosition(InputAction.CallbackContext context)
    {
        Debug.Log("Touch Position");
        TouchPosition = context.ReadValue<Vector2>();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        Debug.Log("Aim");
        
    }

    public void OnAimDirection(InputAction.CallbackContext context)
    {
        AimDirection = context.ReadValue<Vector2>();
    }
}
