using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
    [SerializeField]
    private float speed = 10.0f;
    public int scoreValue = 100;
    public int collisionDamage = 1;

    // A event to notify other classes that it became invisible
    public delegate void EnemyDelegate(GameObject enemy);
    public static event EnemyDelegate BecameInvisible;
    public static event EnemyDelegate EnemyDied;

    public delegate void EnemyTriggerDelegate(GameObject enemy, Collider col);
    public static event EnemyTriggerDelegate TriggerEnter;

	// Use this for initialization
	void OnEnable () 
    {
        GetComponent<Rigidbody>().velocity = Vector3.forward * speed;
	}
	
    void OnBecameInvisible()
    {
        if (BecameInvisible != null)
        {
            BecameInvisible(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (TriggerEnter != null)
        {
            if (EnemyDied != null)
            {
                EnemyDied(this.gameObject);
            }
            TriggerEnter(this.gameObject, col);
        }
    }
}
