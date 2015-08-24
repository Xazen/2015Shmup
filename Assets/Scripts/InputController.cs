using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class KeyboardControl
{
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode down = KeyCode.DownArrow;
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode shoot = KeyCode.Space;
}

public class InputController : MonoBehaviour
{
    #region insprector
    public enum InputType
    {
        Keyboard,
        Mouse,
        Count
    }

    [SerializeField]
    private InputType inputType = InputType.Keyboard;
    public KeyboardControl keyboardKeyCodes;
    #endregion

    #region delegates
    public delegate void KeyCodeDelegate(KeyCode keyCode);
    public delegate void Vector3Delegate(Vector3 vector3);
    
    public KeyCodeDelegate keyDownDelegate;
    public KeyCodeDelegate keyHoldDelegate;
    public KeyCodeDelegate keyUpDelegate;
    public Vector3Delegate mousePositionChangedDelegate;
    #endregion

    private Vector3 currentMousePosition;

    #region logic
    // Update is called once per frame
	protected void Update () 
    {
        // Is keyboard input?
        if (inputType == InputType.Keyboard)
        {
            // Call keyboard control delegate
            this.CallKeyDelegate(keyboardKeyCodes.up);
            this.CallKeyDelegate(keyboardKeyCodes.left);
            this.CallKeyDelegate(keyboardKeyCodes.down);
            this.CallKeyDelegate(keyboardKeyCodes.right);
            this.CallKeyDelegate(keyboardKeyCodes.shoot);
        }
        // Is mouse input?
        else if (inputType == InputType.Mouse)
        {
            // Call delegate for mouse button
            this.CallKeyDelegate(KeyCode.Mouse0);

            // Call mouse position delegate
            if (mousePositionChangedDelegate != null)
            {
                // Call only if mouse position had changed since last update
                if (currentMousePosition != Input.mousePosition)
                {
                    currentMousePosition = Input.mousePosition;
                    mousePositionChangedDelegate(currentMousePosition);
                }
            }
        }
	}

    /// <summary>
    /// Call the delegate with the keyCode if there is a match.
    /// </summary>
    /// <param name="keyCode"></param>
    private void CallKeyDelegate(KeyCode keyCode)
    {
        // Call delegates
        if (Input.GetKeyDown(keyCode))
        {
            if (keyDownDelegate != null)
            {
                keyDownDelegate(keyCode);
            }
        }

        if (Input.GetKey(keyCode))
        {
            if (keyHoldDelegate != null)
            {
                keyHoldDelegate(keyCode);
            }
        }

        if (Input.GetKeyUp(keyCode))
        {
            if (keyUpDelegate != null)
            {
                keyUpDelegate(keyCode);
            }
        }
    }
    #endregion
}
