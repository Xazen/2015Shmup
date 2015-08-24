using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    [SerializeField]
    private float speed = 20.0f;

    private KeyboardControl keyboardControl;
    private Rigidbody rigidbody;
    private Vector3 moveAmount = Vector3.zero;

	// Use this for initialization
	void Start () 
    {
        // Setup keyboard control
        keyboardControl = GameController.InputController.keyboardKeyCodes;
        rigidbody = GetComponent<Rigidbody>();

        // Setup input delegates
        GameController.InputController.keyHoldDelegate += OnKeyHold;
        GameController.InputController.keyUpDelegate += OnKeyUp;
        GameController.InputController.mousePositionChangedDelegate += OnMousePositionChanged;
	}

    protected void FixedUpdate()
    {
        rigidbody.velocity = moveAmount * speed * Time.fixedDeltaTime;
    }

    private void OnKeyHold(KeyCode keyCode)
    {
        if (keyCode == keyboardControl.up)
        {
            moveAmount.z = 1.0f;
        }

        if (keyCode == keyboardControl.right)
        {
            moveAmount.x = 1.0f;
        }

        if (keyCode == keyboardControl.left)
        {
            moveAmount.x = -1.0f;
        }

        if (keyCode == keyboardControl.down)
        {
            moveAmount.z = -1.0f;
        }
        moveAmount = moveAmount.normalized;
    }

    private void OnKeyUp(KeyCode keyCode)
    {
        if (keyCode == keyboardControl.up)
        {
            moveAmount.z = 0.0f;
        }

        if (keyCode == keyboardControl.right)
        {
            moveAmount.x = 0.0f;
        }

        if (keyCode == keyboardControl.left)
        {
            moveAmount.x = 0.0f;
        }

        if (keyCode == keyboardControl.down)
        {
            moveAmount.z = 0.0f;
        }
        moveAmount = moveAmount.normalized;
    }

    private void OnMousePositionChanged(Vector3 newPosition)
    {
        moveAmount = (newPosition-this.transform.position).normalized;   
    }
}
