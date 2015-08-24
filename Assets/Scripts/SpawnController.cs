using UnityEngine;
using System;
using System.Collections;

public class SpawnController : MonoBehaviour 
{
    [SerializeField]
    private ObjectPool enemyPool;
    [SerializeField]
    private float spawnRate = 3.0f;
    private Border gameArea;

	// Use this for initialization
	void Start () 
    {
        // Setup event
        EnemyController.BecameInvisible += OnEnemyBecamInvisible;
        EnemyController.TriggerEnter += OnCollisionEnterEnemy;

        gameArea = GameController.GameArea;

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
        if (col.CompareTag(MainController.Tags.BULLET))
        {
            enemyPool.ReturnGameObject(enemy);
        }
    }
    #endregion
}
