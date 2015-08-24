using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    [SerializeField]
    private float speed = 20.0f;

    // A event to notify other classes that it became invisible
    public delegate void BecomeInvisible(GameObject bullet);
    public static event BecomeInvisible becomeInvisible;

	protected void Start () 
    {
        // Set speed of the bullet
        GetComponent<Rigidbody>().SetVelocityZ(speed);
	}

    protected void OnBecameInvisible()
    {
        if (becomeInvisible != null)
        {
            becomeInvisible(gameObject);
        }
    }
}
