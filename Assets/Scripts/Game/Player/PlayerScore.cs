using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// The score controller manages the score of the player
/// </summary>
public class PlayerScore : MonoBehaviour 
{    
    [SerializeField]
    private Text ScoreUI;

    [HideInInspector]
    public int Score = 0;

    #region setup
	// Use this for initialization
	protected void Start()
    {
        EnemyController.TriggerEnter += OnCollisionEnterEnemy;
    }
    #endregion

    #region event
    public void OnCollisionEnterEnemy(GameObject enemy, Collider col)
    {
        if (col.CompareTag(MainController.Tags.PLAYER_BULLET))
        {
            Score += enemy.GetComponent<EnemyController>().scoreValue;
            ScoreUI.text = Score.ToString();
        }
    }
    #endregion

    #region destroy
    protected void OnDestroy()
    {
        if (ScoreUI != null)
        {
            ScoreUI = null;
        }
    }
    #endregion
}
