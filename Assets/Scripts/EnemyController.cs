using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
    [SerializeField]
    private float speed = 10.0f;

	// Use this for initialization
	void Start () 
    {
        GetComponent<Rigidbody>().velocity = Vector3.forward * speed;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
