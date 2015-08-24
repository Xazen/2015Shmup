using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    // Health
    [SerializeField]
    private PlayerHealth playerHealth;

    public delegate void PlayerTriggerDelegate(GameObject player, Collider col);
    public event PlayerTriggerDelegate triggerEvent;

    // Movement
    [SerializeField]
    private float speed = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 mouseTargetPosition = Vector3.zero;

    private KeyboardControl keyboardControl;
    private Rigidbody playerRigidbody;

    // Fire
    [SerializeField]
    private ObjectPool bulletPool;

    [SerializeField]
    private float holdFireRate = 0.3f;
    
    #region setup
    // Use this for initialization
	void Start () 
    {
        playerRigidbody = GetComponent<Rigidbody>();

        // Setup keyboard control
        keyboardControl = GameController.InputController.keyboardKeyCodes;

        // Setup player health event
        playerHealth.healthDepletedEvent += OnHealthDepleted;

        // Setup bullet event
        PlayerBullet.BecameInvisibleEvent += OnBulletBecameInvisible;
        PlayerBullet.TriggerEvent += OnBulletCollision;

        // Setup input delegates
        GameController.InputController.keyDownDelegate += OnKeyDown;
        GameController.InputController.keyHoldDelegate += OnKeyHold;
        GameController.InputController.keyUpDelegate += OnKeyUp;
        GameController.InputController.mousePositionChangedDelegate += OnMousePositionChanged;
	}
    #endregion

    #region actions
    protected void Update()
    {
        // Update player movement
        if (playerRigidbody.velocity != moveDirection * speed)
        {
            playerRigidbody.velocity = moveDirection * speed;
        }

        // Keep the player inside the game area
        Border gameArea = GameController.GameArea;
        Vector3 playerSize = GetComponent<Collider>().bounds.size;
        if (transform.position.x + playerSize.x / 2 > gameArea.right)
        {
            transform.position = new Vector3(gameArea.right - playerSize.x / 2, 0, transform.position.z);
        } 
        else if (transform.position.x - playerSize.x / 2 < gameArea.left)
        {
            transform.position = new Vector3(gameArea.left + playerSize.x / 2, 0, transform.position.z);
        }

        if (transform.position.z - playerSize.z / 2 < gameArea.bottom)
        {
            transform.position = new Vector3(transform.position.x, 0, gameArea.bottom + playerSize.z / 2);
        }
        else if (transform.position.z + playerSize.z / 2 > gameArea.top)
        {
            transform.position = new Vector3(transform.position.x, 0, gameArea.top - playerSize.z / 2);
        }

        // Stop the player if its close enought to the destination given by the mouse
        if (Vector3.Distance(mouseTargetPosition, transform.position) <= 0.2f)
        {
            moveDirection = Vector3.zero;
        }
    }

    public IEnumerator Fire()
    {
        // Loop fire
        do
        {
            GameObject bullet = bulletPool.GetGameObject();
            bullet.transform.position = this.transform.position;
            bullet.transform.rotation = this.transform.rotation;

            for (float timer = 0; timer <= holdFireRate; timer += Time.fixedDeltaTime)
            {
                yield return new WaitForFixedUpdate();
            }
        } while (true);
    }
    #endregion

    #region collision
    public void OnTriggerEnter(Collider col)
    {
        // Call event
        if (triggerEvent != null)
        {
            triggerEvent(this.gameObject, col);
        }

        // Reduce health
        if (col.CompareTag(MainController.Tags.ENEMY))
        {
            playerHealth.DecreaseLife(col.gameObject.GetComponent<EnemyController>().collisionDamage);
        }
    }
    #endregion

    #region gameplay events
    public void OnHealthDepleted()
    {
        //TODO Show gameover
    }

    public void OnBulletBecameInvisible(GameObject bullet)
    {
        bulletPool.ReturnGameObject(bullet);
    }

    public void OnBulletCollision(GameObject bullet, Collider col)
    {
        if (col.CompareTag(MainController.Tags.ENEMY))
        {
            bulletPool.ReturnGameObject(bullet);
        }
    }
    #endregion

    #region input delegates
    private void OnKeyDown(KeyCode keyCode)
    {
        // Start firing
        if (keyCode == keyboardControl.shoot ||
            keyCode == KeyCode.Mouse0)
        {
            StartCoroutine("Fire");
        }
    }

    private void OnKeyHold(KeyCode keyCode)
    {
        if (keyCode == keyboardControl.up)
        {
            moveDirection.z = 1.0f;
        }

        if (keyCode == keyboardControl.right)
        {
            moveDirection.x = 1.0f;
        }

        if (keyCode == keyboardControl.left)
        {
            moveDirection.x = -1.0f;
        }

        if (keyCode == keyboardControl.down)
        {
            moveDirection.z = -1.0f;
        }
        moveDirection = moveDirection.normalized;
    }

    private void OnKeyUp(KeyCode keyCode)
    {
        // Update move amount
        if (keyCode == keyboardControl.up)
        {
            moveDirection.z = 0.0f;
        }

        if (keyCode == keyboardControl.right)
        {
            moveDirection.x = 0.0f;
        }

        if (keyCode == keyboardControl.left)
        {
            moveDirection.x = 0.0f;
        }

        if (keyCode == keyboardControl.down)
        {
            moveDirection.z = 0.0f;
        }
        moveDirection = moveDirection.normalized;

        // Stop firing
        if (keyCode == keyboardControl.shoot ||
            keyCode == KeyCode.Mouse0)
        {
            StopCoroutine("Fire");
        }
    }

    private void OnMousePositionChanged(Vector3 newPosition)
    {
        mouseTargetPosition = newPosition;
        moveDirection = (newPosition-this.transform.position).normalized;
    }
    #endregion
}