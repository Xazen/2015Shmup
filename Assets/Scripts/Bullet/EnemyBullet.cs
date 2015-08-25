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

    private Vector3 moveDirection;

    // Target
    private GameObject target;

    protected void Awake()
    {
        target = GameController.instance.player;
    }

    protected void OnEnable()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        StartCoroutine(TargetedFire());
    }

    private IEnumerator TargetedFire()
    {
        for (float timer = 0.0f; timer <= delay; timer += Time.deltaTime)
        {
            yield return 0;
        }

        moveDirection = (target.transform.position - this.transform.position).normalized;
        this.GetComponent<Rigidbody>().velocity = moveDirection * speed;
    }
}
