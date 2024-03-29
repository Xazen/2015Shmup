﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Controls the highscore scene.
/// </summary>
public class HighscoreSceneController : SceneController 
{
    [SerializeField]
    private GameObject highscoreTable;

    [SerializeField]
    private int maxNameLength = 12;

    protected void Start()
    {
        // Loads the high score to the view
        for (int i = 0; i < highscoreTable.transform.childCount; i++)
        {
            // Get the highscore row
            GameObject row = highscoreTable.transform.GetChild(i).gameObject;

            // Get high score entry
            HighscoreEntry highscoreEntry = MainController.HighscoreController.highscoreEntries[i];

            // Get the name text ui
            Text nameText = row.transform.GetChild(1).gameObject.GetComponent<Text>();

            // Set the name with max 10 characters
            nameText.text = highscoreEntry.Name.Substring(0, Mathf.Min(highscoreEntry.Name.Length, maxNameLength));

            // Assign score to text ui
            Text scoreText = row.transform.GetChild(2).gameObject.GetComponent<Text>();
            scoreText.text = highscoreEntry.Score.ToString();
        }
    }

    #region destroy
    protected void OnDestroy()
    {
        if (highscoreTable != null)
        {
            highscoreTable = null;
        }
    }
    #endregion
}
