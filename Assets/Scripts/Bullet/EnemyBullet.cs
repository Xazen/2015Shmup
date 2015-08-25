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
    private float delay = 0.3f;
    public int damage = 1;

    private Vector3 moveDirection;

    // Target
    private GameObject target;

    protected void Awake()
    {
        target = GameController.instance.player;
    }

    protected void OnEnable()
    {
        // The velocity of the bullet is set back to zero first
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

        // The targeted fire gives the player the opportunity to react
        StartCoroutine(TargetedFire());
    }

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
}
