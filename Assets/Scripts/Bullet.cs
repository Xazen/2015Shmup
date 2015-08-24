using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    [SerializeField]
    private float speed = 20.0f;

    // A event to notify other classes that it became invisible
    public delegate void BulletDelegate(GameObject bullet);
    public static event BulletDelegate BecameInvisible;

    public delegate void BulletCollisionDelegate(GameObject bullet, Collision col);
    public static event BulletCollisionDelegate CollisionDelegate;

	protected void OnEnable() 
    {
        // Set speed of the bullet
        GetComponent<Rigidbody>().SetVelocityZ(speed);
	}

    #region delegate methods
    protected void OnBecameInvisible()
    {
        if (BecameInvisible != null)
        {
            BecameInvisible(this.gameObject);
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        CollisionDelegate(this.gameObject, col);
    }
    #endregion
}
