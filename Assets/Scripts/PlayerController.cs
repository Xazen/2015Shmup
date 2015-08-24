using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    [SerializeField]
    private float speed = 20.0f;
    
    private Vector3 moveAmount = Vector3.zero;
    private Vector3 mouseTargetPosition = Vector3.zero;

    private KeyboardControl keyboardControl;
    private Rigidbody playerRigidbody;


	// Use this for initialization
	void Start () 
    {
        // Setup keyboard control
        keyboardControl = GameController.InputController.keyboardKeyCodes;
        playerRigidbody = GetComponent<Rigidbody>();

        // Setup input delegates
        GameController.InputController.keyHoldDelegate += OnKeyHold;
        GameController.InputController.keyUpDelegate += OnKeyUp;
        GameController.InputController.mousePositionChangedDelegate += OnMousePositionChanged;
	}

    protected void Update()
    {
        // Update player movement
        if (playerRigidbody.velocity != moveAmount * speed)
        {
            playerRigidbody.velocity = moveAmount * speed;
        }

        // Stop the player if its close enought to the destination given by the mouse
        if (Vector3.Distance(mouseTargetPosition, transform.position) <= 0.2f)
        {
            moveAmount = Vector3.zero;
        }
    }

    #region input delegates
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
        mouseTargetPosition = newPosition;
        moveAmount = (newPosition-this.transform.position).normalized;
    }
    #endregion
}
