using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float fireRate = 3.0f;

    public int scoreValue = 100;
    public int collisionDamage = 1;

    // A event to notify other classes that it became invisible
    public delegate void EnemyDelegate(GameObject enemy);
    public static event EnemyDelegate BecameInvisible;
    public static event EnemyDelegate EnemyDied;

    public delegate void EnemyTriggerDelegate(GameObject enemy, Collider col);
    public static event EnemyTriggerDelegate TriggerEnter;

    protected void Start()
    {

    }

    /// <summary>
    /// Start firing bullets
    /// </summary>
    /// <returns></returns>
    private IEnumerator Fire()
    {
        while (true) 
        {
            // Fire a bullet
            //GameObject bullet = bulletPool.GetGameObject();
            //bullet.transform.position = this.transform.position;

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
        // Stop firing
        StopCoroutine("Fire");
        
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
