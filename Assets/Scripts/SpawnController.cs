using UnityEngine;
using System;
using System.Collections;

#region border implementation
/// <summary>
/// Border class is used to determine a border. e.g. game viewport.
/// </summary>
[Serializable]
public struct Border
{
    public float top;
    public float left;
    public float bottom;
    public float right;

    public Border(float up, float left, float down, float right)
    {
        this.top = up;
        this.left = left;
        this.bottom = down;
        this.right = right;
    }

    public void Print()
    {
        Debug.Log ("Border: " + this + "\n" +
            "top    : " + this.top + "\n" +
            "left   : " + this.left + "\n" +
            "bottom : " + this.bottom + "\n" +
            "right  : " + this.right);
    }
}
#endregion

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
        // Get reference points to setup game area
        Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.y));
        Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.y));

        // Setup game area
        gameArea = new Border(
            upperRight.z,
            lowerLeft.x,
            lowerLeft.z,
            upperRight.x
            );

        // Setup event
        EnemyController.BecameInvisible += OnEnemyBecamInvisible;
        EnemyController.CollisionEnter += OnCollisionEnterEnemy;

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
                gameArea.top + enemyColliderSize.z);

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

    public void OnCollisionEnterEnemy(GameObject enemy, Collision col)
    {
        if (col.collider.CompareTag(MainController.Tags.BULLET))
        {
            enemyPool.ReturnGameObject(enemy);
        }
    }
    #endregion
}
