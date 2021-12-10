using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.UI.Enable();
    }
    private void FixedUpdate()
    {
        Debug.Log(inputActions.UI.Mouse.ReadValue<Vector2>());
    }
}
