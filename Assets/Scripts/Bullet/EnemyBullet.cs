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
    private Vector3 moveDirection;
    
    // Target
    private GameObject target;

    protected void Start()
    {
        //Debug.Log("target start: " + GameController.instance.player);
        //target = GameController.instance.player;
    }

    protected void OnEnable()
    {
        //Debug.Log("target: " + target);
        //moveDirection = (target.transform.position - this.transform.position).normalized;

        this.GetComponent<Rigidbody>().SetVelocityZ(-speed);
    }
}
