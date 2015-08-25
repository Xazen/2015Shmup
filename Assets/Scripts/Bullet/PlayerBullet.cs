using UnityEngine;
using System.Collections;

public class PlayerBullet : Bullet 
{
    [SerializeField]
    private float speed = 20.0f;

	protected void OnEnable() 
    {
        // Set speed of the bullet
        GetComponent<Rigidbody>().SetVelocityZ(speed);
	}
}
