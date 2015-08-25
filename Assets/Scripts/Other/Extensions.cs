using UnityEngine;
using System.Collections;

public static class Extensions
{
    #region rigidbody
    public static void SetVelocityX(this Rigidbody r, float velocityX)
    {
        r.velocity = new Vector3(velocityX, r.velocity.y, r.velocity.z);
    }

    public static void SetVelocityY(this Rigidbody r, float velocityY)
    {
        r.velocity = new Vector3(r.velocity.x, velocityY, r.velocity.z);
    }

    public static void SetVelocityZ(this Rigidbody r, float velocityZ)
    {
        r.velocity = new Vector3(r.velocity.x, r.velocity.y, velocityZ);
    }
    #endregion
}