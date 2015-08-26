using UnityEngine;
using System.Collections;

/// <summary>
/// The enemey class manages the enemy move and fire behavior
/// </summary>
public class EnemyController : MonoBehaviour
{
    #region variables
    // Movement
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float maxFireRate = 2.5f;
    [SerializeField]
    private float minFireRate = 0.5f;
    [SerializeField]
    private float maxFirstFireDelay = 1.5f;
    [SerializeField]
    private float minFirstFireDelay = 0.5f;

    private Vector3 moveDirection = Vector3.zero;

    // Spawn location
    public enum SpawnPosition
    {
        Top,
        Left,
        Bottom,
        Right,
        Count
    }
    
    [HideInInspector]
    public SpawnPosition spawnPosition = SpawnPosition.Top;

    // Public variable
    public int scoreValue = 100;
    public int collisionDamage = 1;

    // A event to notify other classes that it became invisible
    public delegate void EnemyDelegate(GameObject enemy);
    public static event EnemyDelegate BecameInvisible;
    public static event EnemyDelegate EnemyDied;

    public delegate void EnemyTriggerDelegate(GameObject enemy, Collider col);
    public static event EnemyTriggerDelegate TriggerEnter;

    public delegate GameObject EnemyBulletDelegate(GameObject enemy);
    public static event EnemyBulletDelegate BulletOfEnemy;
    #endregion

    #region setup
    private void Update()
    {
        // Stop firing when the enemy become out of the game area
        if (this.WillBecomeInvisible())
        {
            StopCoroutine("Fire");
        }
    }

    void OnEnable()
    {

        // Start firing
        StartCoroutine("Fire");
    }
    #endregion

    #region actions
    public void CalculateMoveDirection()
    {
        // Set velocity when the object is enabled
        switch (spawnPosition)
        {
            case SpawnPosition.Left:
                moveDirection = Vector3.left;
                transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case SpawnPosition.Bottom:
                moveDirection = Vector3.back;
                transform.rotation = Quaternion.identity;
                break;
            case SpawnPosition.Right:
                moveDirection = Vector3.right;
                transform.rotation = Quaternion.Euler(0, 270, 0);
                break;
            case SpawnPosition.Top:
            default:
                moveDirection = Vector3.forward;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
        }
        GetComponent<Rigidbody>().velocity = moveDirection * speed;
    }

    /// <summary>
    /// Start firing bullets
    /// </summary>
    /// <returns></returns>
    private IEnumerator Fire()
    {
        // Wait for first fire delay
        float firstFireDelay = UnityEngine.Random.Range(minFirstFireDelay, maxFirstFireDelay);
        for (float timer = 0; timer <= firstFireDelay; timer += Time.fixedDeltaTime)
        {
            yield return new WaitForFixedUpdate();
        }

        // Fire more bullets
        while (true) 
        {
            // Get bullet
            GameObject bullet = BulletOfEnemy(this.gameObject);

            // Bullet positioning
            bullet.transform.position = this.transform.position;

            // Position the bullet in front of the enemy so that it become visible
            bullet.transform.SetPositionZ(bullet.transform.position.z);

            // Make sure the rotation is the same as the enemy rotation
            bullet.transform.rotation = this.transform.rotation;
         
            // Wait for fire rate
            float fireRate = UnityEngine.Random.Range(minFireRate, maxFireRate);
            for (float timer = 0; timer <= fireRate; timer += Time.fixedDeltaTime)
            {
                yield return new WaitForFixedUpdate();
            }
        }
    }
    #endregion
   
    #region helper
    /// <summary>
    /// Based on the spawn position the method will return true when the enemy is at the border 
    /// of the game area
    /// </summary>
    /// <returns>Return true when the enemy is at the border. False otherwise</returns>
    private bool WillBecomeInvisible()
    {
        Border gameArea = GameSceneController.instance.gameArea;
        Vector3 enemySize = GetComponent<Collider>().bounds.size;

        if ((transform.position.x + enemySize.x / 2 > gameArea.right &&
            spawnPosition == SpawnPosition.Left) ||
            (transform.position.x - enemySize.x / 2 < gameArea.left &&
            spawnPosition == SpawnPosition.Right) ||
            (transform.position.z - enemySize.z / 2 < gameArea.bottom &&
            spawnPosition == SpawnPosition.Top) ||
            (transform.position.z + enemySize.z / 2 > gameArea.top &&
            spawnPosition == SpawnPosition.Bottom))
        {
            return true;
        }

        return false;
    }
    #endregion

    #region events
    void OnBecameInvisible()
    {      
        // Send became invisible event
        if (BecameInvisible != null)
        {
            BecameInvisible(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        // Send trigger event
        if (TriggerEnter != null)
        {
            TriggerEnter(this.gameObject, col);
        }
        
        // Send die event
        if (EnemyDied != null)
        {
            EnemyDied(this.gameObject);
        }
    }
    #endregion

    #region destroy
    protected void OnDestroy()
    {
        if (BecameInvisible != null)
        {
            BecameInvisible = null;
        }

        if (EnemyDied != null)
        {
            EnemyDied = null;
        }
        
        if (TriggerEnter != null)
        {
            TriggerEnter = null;
        }

        if (BulletOfEnemy != null)
        {
            BulletOfEnemy = null;
        }
        StopAllCoroutines();
    }
    #endregion
}
