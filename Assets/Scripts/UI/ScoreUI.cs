using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Manages the score ui
/// </summary>
public class ScoreUI : MonoBehaviour 
{
    private Text scoreText;

    #region setup
    // Use this for initialization
	void Start () 
    {
        scoreText = GetComponent<Text>();
	}
    #endregion

    #region action
    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
    #endregion

    #region destroy
    protected void OnDestroy()
    {
        if (scoreText != null)
        {
            scoreText = null;
        }
    }
    #endregion
}
