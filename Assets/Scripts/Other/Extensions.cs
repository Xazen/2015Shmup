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

    #region transform
    public static void SetPositionX(this Transform t, float newPositionX)
    {
        t.position = new Vector3(newPositionX, t.position.y, t.position.z);
    }

    public static void SetPositionY(this Transform t, float newPositionY)
    {
        t.position = new Vector3(t.position.x, newPositionY, t.position.z);
    }

    public static void SetPositionZ(this Transform t, float newPositionZ)
    {
        t.position = new Vector3(t.position.x, t.position.y, newPositionZ);
    }
    #endregion
}