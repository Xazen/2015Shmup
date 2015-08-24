using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    [SerializeField]
    private float speed = 20.0f;

    // A event to notify other classes that it became invisible
    public delegate void BulletDelegate(GameObject bullet);
    public static event BulletDelegate BecameInvisible;

	protected void Start () 
    {
        // Set speed of the bullet
        GetComponent<Rigidbody>().SetVelocityZ(speed);
	}

    protected void OnBecameInvisible()
    {
        if (BecameInvisible != null)
        {
            BecameInvisible(gameObject);
        }
    }
}
