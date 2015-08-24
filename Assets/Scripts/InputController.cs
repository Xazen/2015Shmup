using UnityEngine;
using System;
using System.Collections;

[Serializable]
class KeyboardControl
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

    [SerializeField]
    private KeyboardControl keyboardKeyCodes;
    #endregion

    #region delegates
    private delegate void KeyCodeDelegate(KeyCode keyCode);
    private delegate void Vector3Delegate(Vector3 vector3);
    private KeyCodeDelegate keyDownDelegate;
    private KeyCodeDelegate keyHoldDelegate;
    private KeyCodeDelegate keyUpDelegate;
    private Vector3Delegate mousePositionDelegate;
    #endregion

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

            if (mousePositionDelegate != null)
            {
                mousePositionDelegate(Input.mousePosition);
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
                keyDownDelegate(keyCode);
            }
        }
    }
    #endregion
}
