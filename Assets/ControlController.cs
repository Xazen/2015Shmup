using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlController : MonoBehaviour 
{

    [SerializeField]
    private Toggle mouseToggle;
    [SerializeField]
    private Toggle keyboardToggle;

    private InputController inputController;

    #region setup
    protected void Start()
    {
        inputController = GameController.instance.inputController;

        this.Initialize();
    }

    protected void OnEnable () 
    {
        if (inputController != null)
        {
            this.Initialize();
        }
    }

    private void Initialize()
    {
        // Set callbacks
        mouseToggle.onValueChanged.AddListener(OnMouseValueChanged);
        keyboardToggle.onValueChanged.AddListener(OnKeyboardValueChanged);

        // Set toggle base on current input type
        if (inputController.inputType == InputController.InputType.Mouse)
        {
            mouseToggle.isOn = true;
        }
        else
        {
            keyboardToggle.isOn = true;
        }
    }

    #endregion
    /// <summary>
    /// Handles mouse toggle event
    /// </summary>
    private void OnMouseValueChanged(bool value)
    {
        mouseToggle.isOn = value;
        if (value)
        {
            inputController.inputType = InputController.InputType.Mouse;
        }
        else
        {
            inputController.inputType = InputController.InputType.Keyboard;
        }
    }

    /// <summary>
    /// Handles keyboard toggle event
    /// </summary>
    private void OnKeyboardValueChanged(bool value)
    {
        keyboardToggle.isOn = value;
        if (value)
        {
            inputController.inputType = InputController.InputType.Keyboard;
        }
        else
        {
            inputController.inputType = InputController.InputType.Mouse;
        }
    }
}
