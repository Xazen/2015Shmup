using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Helper class to setup the keys to control the game with the keyboard
/// </summary>
[Serializable]
public class KeyboardControl
{
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode down = KeyCode.DownArrow;
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode shoot = KeyCode.Space;
}

/// <summary>
/// Manages the input callbacks
/// </summary>
public class InputController : MonoBehaviour
{
    #region inspector
    public enum InputType
    {
        Keyboard,
        Mouse,
        Count
    }

    [SerializeField]
    private InputType gameInputType = InputType.Keyboard;
    public InputType GameInputType
    {
        set
        {
            if (onGameInputTypeChanged != null)
            {
                onGameInputTypeChanged(value);
            }
            gameInputType = value;
        }

        get
        {
            return gameInputType;
        }
    }

    public KeyboardControl keyboardKeyCodes;

    #endregion

    #region delegates
    public delegate void KeyCodeDelegate(KeyCode keyCode);
    
    public KeyCodeDelegate keyDownDelegate;
    public KeyCodeDelegate keyHoldDelegate;
    public KeyCodeDelegate keyUpDelegate;

    public delegate void Vector3Delegate(Vector3 vector3);
    public Vector3Delegate mousePositionChangedDelegate;

    public delegate void InputTypeDelegate(InputType inputType);
    public event InputTypeDelegate onGameInputTypeChanged;
    #endregion

    private Vector3 currentMousePosition;

    #region logic
    // Update is called once per frame
	protected void Update () 
    {
        // Is keyboard input?
        if (this.GameInputType == InputType.Keyboard)
        {
            // Call keyboard control delegate
            this.CallKeyDelegate(keyboardKeyCodes.up);
            this.CallKeyDelegate(keyboardKeyCodes.left);
            this.CallKeyDelegate(keyboardKeyCodes.down);
            this.CallKeyDelegate(keyboardKeyCodes.right);
            this.CallKeyDelegate(keyboardKeyCodes.shoot);
        }
        // Is mouse input?
        else if (this.GameInputType == InputType.Mouse)
        {
            // Call delegate for mouse button
            this.CallKeyDelegate(KeyCode.Mouse0);

            // Call mouse position delegate
            if (mousePositionChangedDelegate != null)
            {
                // Call only if mouse position had changed since last update
                if (currentMousePosition != Input.mousePosition)
                {
                    // Get the screen space for x and y
                    currentMousePosition = Input.mousePosition;

                    // Set the z value to position the mouse position in 3D space
                    // It should be on the same level as the player. Since the player is positioned at zero, the required z value is the same as the camera's y value
                    Vector3 mousePositionIn3DSpace = currentMousePosition;
                    mousePositionIn3DSpace.z = Camera.main.transform.position.y;

                    // Convert mouse positon from screen space to world space
                    Vector3 mouseWorldSpace = Camera.main.ScreenToWorldPoint(mousePositionIn3DSpace);

                    // Set y to 0 since it is not required for the player movement.
                    mouseWorldSpace.y = 0;

                    // Call delegate methods
                    mousePositionChangedDelegate(mouseWorldSpace);
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

    #region destroy
    protected void OnDestroy()
    {
        if (keyDownDelegate != null)
        {
            keyDownDelegate = null;
        }

        if (keyHoldDelegate != null)
        {
            keyHoldDelegate = null;
        }

        if (keyUpDelegate != null)
        {
            keyUpDelegate = null;
        }

        if (mousePositionChangedDelegate != null)
        {
            mousePositionChangedDelegate = null;
        }
    }
    #endregion
}
