using UnityEngine;
using System.Collections;

/// <summary>
/// The enemey class manages the enemy move and fire behavior
/// </summary>
public class EnemyController : MonoBehaviour 
{
    public enum SpawnPosition
    {
        Top,
        Left,
        Bottom,
        Right,
        Count
    }

    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float fireRate = 1.0f;
    [SerializeField]
    private float firstFireDelay = 0.5f;

    [HideInInspector]
    public SpawnPosition spawnPosition = SpawnPosition.Top;

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

    private void Update()
    {
        // Stop firing when the enemy become out of the game area
        if (this.WillBecomeInvisible())
        {
            StopCoroutine("Fire");
        }
    }

    /// <summary>
    /// Based on the spawn position the method will return true when the enemy is at the border 
    /// of the game area
    /// </summary>
    /// <returns>Return true when the enemy is at the border. False otherwise</returns>
    private bool WillBecomeInvisible()
    {
        Border gameArea = GameController.instance.gameArea;
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

    /// <summary>
    /// Start firing bullets
    /// </summary>
    /// <returns></returns>
    private IEnumerator Fire()
    {
        // Wait for first fire delay
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
            Vector3 enemySize = GetComponent<Collider>().bounds.size;
            bullet.transform.position = this.transform.position;

            // Position the bullet in front of the enemy so that it become visible
            bullet.transform.SetPositionZ(bullet.transform.position.z);

            // Make sure the rotation is the same as the enemy rotation
            bullet.transform.rotation = this.transform.rotation;
         
            // Wait for fire rate
            for (float timer = 0; timer <= fireRate; timer += Time.fixedDeltaTime)
            {
                yield return new WaitForFixedUpdate();
            }
        }
    }

	void OnEnable() 
    {
        // Set velocity when the object is enabled
        GetComponent<Rigidbody>().velocity = Vector3.forward * speed;
        
        // Start firing
        StartCoroutine("Fire");
	}
	
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
}
