using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreUI : MonoBehaviour 
{
    private Text scoreText;
	// Use this for initialization
	void Start () 
    {
        scoreText = GetComponent<Text>();
	}

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
