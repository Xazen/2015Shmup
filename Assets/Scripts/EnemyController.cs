using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
    [SerializeField]
    private float speed = 10.0f;
    public int scoreValue = 100;

    // A event to notify other classes that it became invisible
    public delegate void EnemyDelegate(GameObject enemy);
    public static event EnemyDelegate BecameInvisible;
    public static event EnemyDelegate EnemyDied;

    public delegate void EnemyCollisionDelegate(GameObject enemy, Collision col);
    public static event EnemyCollisionDelegate CollisionEnter;

	// Use this for initialization
	void Start () 
    {
        GetComponent<Rigidbody>().velocity = Vector3.forward * speed;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    protected void OnBecameInvisible()
    {
        if (BecameInvisible != null)
        {
            BecameInvisible(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        if (CollisionEnter != null)
        {
            if (EnemyDied != null)
            {
                EnemyDied(this.gameObject);
            }
            CollisionEnter(this.gameObject, col);
        }
    }
}
