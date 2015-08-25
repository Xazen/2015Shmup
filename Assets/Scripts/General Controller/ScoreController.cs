using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour 
{
    private static ScoreController _instance;
    
    [HideInInspector]
    public ScoreUI scoreUi;

    private int score = 0;

    #region Singleton
    public static ScoreController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ScoreController>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            // Make it a Singleton if this is the first instance
            _instance = this;
        }
        else
        {
            // In case there is another instance, destroy it
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }
    #endregion

    protected void Start()
    {
        EnemyController.TriggerEnter += OnCollisionEnterEnemy;
    }   

	// Use this for initialization
	public void Initialize(ScoreUI scoreUi)
    {
        this.scoreUi = scoreUi;
    }

    public void OnCollisionEnterEnemy(GameObject enemy, Collider col)
    {
        if (col.CompareTag(MainController.Tags.PLAYER_BULLET))
        {
            score += enemy.GetComponent<EnemyController>().scoreValue;
            scoreUi.SetScore(score);
        }
    }
}
