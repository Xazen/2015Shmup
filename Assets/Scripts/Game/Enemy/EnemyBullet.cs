using UnityEngine;
using System.Collections;

/// <summary>
/// An enemy bullet is similar to a guided missile. When it spawns it flies towards the target.
/// </summary>
public class EnemyBullet : Bullet 
{
    // Movement
    [SerializeField]
    private float speed = 6.0f;
    [SerializeField]
    private float delay = 0.15f;
    public int damage = 1;

    // Determines movement direction
    private Vector3 moveDirection;

    // Target of the missle shot
    private GameObject target;

    #region setup
    protected void Awake()
    {
        // Get the player
        target = GameSceneController.instance.player;
    }

    protected void OnEnable()
    {
        // The velocity of the bullet is set back to zero first
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

        // The targeted fire gives the player the opportunity to react
        StartCoroutine(TargetedFire());
    }
    #endregion

    #region actions
    /// <summary>
    /// The bullet flies directly towards the target's current location
    /// </summary>
    /// <returns></returns>
    private IEnumerator TargetedFire()
    {
        // Wait for delay
        for (float timer = 0.0f; timer <= delay; timer += Time.deltaTime)
        {
            yield return 0;
        }

        // Fires in the direction of the player
        moveDirection = (target.transform.position - this.transform.position).normalized;
        this.GetComponent<Rigidbody>().velocity = moveDirection * speed;
    }
    #endregion

    #region destroy
    public override void OnDestroy()
    {
        base.OnDestroy();
        if (target != null)
        {
            target = null;
        }
    }
    #endregion
}
