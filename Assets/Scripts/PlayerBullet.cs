using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour 
{
    [SerializeField]
    private float speed = 20.0f;

    // A event to notify other classes that it became invisible
    public delegate void BulletDelegate(GameObject bullet);
    public static event BulletDelegate BecameInvisibleEvent;

    public delegate void BulletTriggerDelegate(GameObject bullet, Collider col);
    public static event BulletTriggerDelegate TriggerEvent;

	protected void OnEnable() 
    {
        // Set speed of the bullet
        GetComponent<Rigidbody>().SetVelocityZ(speed);
	}

    #region delegate methods
    /// <summary>
    /// This is used on an empty parent object to group objects. Make sure that the bullet is invisible in the scene view as well (not only game view) otherwise this method will not be triggered.
    /// </summary>
    void OnBecameInvisible()
    {
        if (BecameInvisibleEvent != null)
        {
            BecameInvisibleEvent(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        TriggerEvent(this.gameObject, col);
    }
    #endregion
}
