using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputActions;

[CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObjects/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<bool> TouchEvent;
    public Vector2 TouchPosition { get; private set; }
    public Vector2 AimDirection { get; private set; }

    public void OnTouch(InputAction.CallbackContext context)
    {
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
        Debug.Log("Aiming " + context.ReadValue<Vector2>());
        AimDirection = context.ReadValue<Vector2>();
    }
}
