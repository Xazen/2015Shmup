using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScore : MonoBehaviour 
{
    [SerializeField]
    private ScoreUI scoreUi;

    private int score = 0;

	// Use this for initialization
	void Start () 
    {
        EnemyController.CollisionEnter += OnCollisionEnterEnemy;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    public void OnCollisionEnterEnemy(GameObject enemy, Collision col)
    {
        if (col.collider.CompareTag(MainController.Tags.BULLET))
        {
            score += enemy.GetComponent<EnemyController>().scoreValue;
            scoreUi.SetScore(score);
        }
    }
}
