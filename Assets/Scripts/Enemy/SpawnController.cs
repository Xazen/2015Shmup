﻿using UnityEngine;
using System.Collections;

/// <summary>
/// The SpawnController manages enemy spawns and despawns as well as the spawn and despawn of their bullets.
/// </summary>
public class SpawnController : MonoBehaviour 
{
    private ObjectPool enemyPool;
    private ObjectPool enemyBulletPool;

    [SerializeField]
    private float spawnRate = 3.0f;
    private Border gameArea;

    #region setup
    // Use this for initialization
	void Start () 
    {
        // Setup object pools
        SetupObjectPools();

        // Setup events
        EnemyController.BecameInvisible += OnEnemyBecameInvisible;
        EnemyController.TriggerEnter += OnEnemyTriggerEnter;
        EnemyController.BulletOfEnemy += BulletOfEnemy;

        Bullet.BecameInvisibleEvent += OnBulletBecameInvisible;
        Bullet.TriggerEvent += OnBulletTriggerEnter;

        // Get game area
        gameArea = GameController.instance.gameArea;

        // Spawn enemies
        StartCoroutine("SpawnEnemies");
	}

    /// <summary>
    /// Distinguish between the object pools, assign and initialize them.
    /// </summary>
    public void SetupObjectPools()
    {
        // Get object pools
        ObjectPool[] objectPools = this.GetComponents<ObjectPool>();
        for (int i = 0; i < objectPools.Length; i++)
        {
            if (objectPools[i].pooledGameObject.CompareTag(MainController.Tags.ENEMY))
            {
                // Assign enemy object pool
                enemyPool = objectPools[i];
            }
            else if (objectPools[i].pooledGameObject.CompareTag(MainController.Tags.ENEMY_BULLET))
            {
                // Assign enemy bullet object pool
                enemyBulletPool = objectPools[i];
            }
        }
    }
    #endregion

    #region action
    /// <summary>
    /// Start spawning enemies
    /// </summary>
    public IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Get an enemy from object pool
            GameObject enemy = enemyPool.GetGameObject();

            // Setup position
            Vector3 enemyColliderSize = enemy.GetComponent<Collider>().bounds.size;
            Vector3 enemyPosition = new Vector3(
                UnityEngine.Random.Range(gameArea.left+enemyColliderSize.x/2, gameArea.right-enemyColliderSize.x/2),
                0,
                gameArea.top + enemyColliderSize.z);

            enemy.transform.position = enemyPosition;

            // Wait for spawn rate
            for (float timer = 0; timer < spawnRate; timer += Time.fixedDeltaTime)
            {
                yield return new WaitForFixedUpdate();
            }
        }
    }
    #endregion

    #region enemy and bullet events
    /// <summary>
    /// This event is called when an enemy requires a bullet
    /// </summary>
    /// <param name="gameObject">The enemy game object</param>
    /// <returns>The bullet the enemy should use</returns>
    public GameObject BulletOfEnemy(GameObject gameObject)
    {
        // Returns a bullet from the enemy bullet pool
        return enemyBulletPool.GetGameObject();
    }

    /// <summary>
    /// The event is triggered when an enemy became invisible
    /// </summary>
    /// <param name="gameObject">The game object that became invisible</param>
    public void OnEnemyBecameInvisible(GameObject gameObject)
    {
        enemyPool.ReturnGameObject(gameObject);
    }

    /// <summary>
    /// The event is triggered when a bullet became invisible
    /// </summary>
    /// <param name="gameObject">The game object that became invisible</param>
    public void OnBulletBecameInvisible(GameObject gameObject)
    {
        // Enemy bullet became invisible
        if (gameObject.CompareTag(MainController.Tags.ENEMY_BULLET))
        {
            enemyBulletPool.ReturnGameObject(gameObject);
        }
    }

    /// <summary>
    /// The event gets called when an enemy or registered a collider
    /// </summary>
    /// <param name="gameObject">The game object got triggered</param>
    /// <param name="col">The collider that triggered the event</param>
    public void OnEnemyTriggerEnter(GameObject gameObject, Collider col)
    {
        // Enemy got hit by player bullet?
        if (col.CompareTag(MainController.Tags.PLAYER_BULLET))
        {
            // Return enemy to pool
            enemyPool.ReturnGameObject(gameObject);
        }
    }

    /// <summary>
    /// The event gets called when a bullet registered a collider
    /// </summary>
    /// <param name="gameObject">The game object got triggered</param>
    /// <param name="col">The collider that triggered the event</param>
    public void OnBulletTriggerEnter(GameObject gameObject, Collider col)
    {
        // Enemy bullet hit player?
        if (gameObject.CompareTag(MainController.Tags.ENEMY_BULLET) &&
            col.CompareTag(MainController.Tags.PLAYER))
        {
            enemyBulletPool.ReturnGameObject(gameObject);
        }
    }
    #endregion
}
