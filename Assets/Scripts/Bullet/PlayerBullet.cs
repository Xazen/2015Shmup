using UnityEngine;
using System.Collections;

/// <summary>
/// The player bullet just flies forward
/// </summary>
public class PlayerBullet : Bullet 
{
    [SerializeField]
    private float speed = 20.0f;

    #region actions
    protected void OnEnable() 
    {
        // Set speed of the bullet
        GetComponent<Rigidbody>().SetVelocityZ(speed);
    }
    #endregion
}
