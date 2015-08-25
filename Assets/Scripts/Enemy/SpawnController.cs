using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour 
{
    private ObjectPool enemyPool;
    private ObjectPool enemyBulletPool;

    [SerializeField]
    private float spawnRate = 3.0f;
    private Border gameArea;

	// Use this for initialization
	void Start () 
    {
        // Get object pool
        ObjectPool[] objectPools = this.GetComponents<ObjectPool>();
        for(int i = 0; i < objectPools.Length; i++)
        {
            if (objectPools[i].pooledGameObject.CompareTag(MainController.Tags.ENEMY))
            {
                enemyPool = objectPools[i];
            }
            else if (objectPools[i].pooledGameObject.CompareTag(MainController.Tags.ENEMY_BULLET))
            {
                enemyBulletPool = objectPools[i];
            }
        }
        enemyPool.Initialize();
        enemyBulletPool.Initialize();

        // Setup event
        EnemyController.BecameInvisible += OnEnemyBecamInvisible;
        EnemyController.TriggerEnter += OnCollisionEnterEnemy;

        gameArea = GameController.instance.gameArea;

        // Spawn enemies
        StartCoroutine("SpawnEnemies");
	}

    #region enemy spawn and despawn
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
                gameArea.top - enemyColliderSize.z);

            enemy.transform.position = enemyPosition;

            // Wait for spawn rate
            for (float timer = 0; timer < spawnRate; timer += Time.fixedDeltaTime)
            {
                yield return new WaitForFixedUpdate();
            }
        }
    }

    public void OnEnemyBecamInvisible(GameObject enemy)
    {
        enemyPool.ReturnGameObject(enemy);
    }

    public void OnCollisionEnterEnemy(GameObject enemy, Collider col)
    {
        if (col.CompareTag(MainController.Tags.PLAYER_BULLET))
        {
            enemyPool.ReturnGameObject(enemy);
        }
    }
    #endregion
}
