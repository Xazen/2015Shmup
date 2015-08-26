using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Controls the game over screen
/// </summary>
public class GameOverSceneController : SceneController 
{
    [SerializeField]
    private Text highscoreEntryText;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private InputField inputField;

    #region setup
    public void Awake()
    {
        // Insert the score of the player to the text
        scoreText.text = MainController.HighscoreController.RecentScore.ToString();

        if (!MainController.HighscoreController.IsValid(MainController.HighscoreController.RecentScore))
        {
            highscoreEntryText.text = "Final Score";
            inputField.gameObject.SetActive(false);
        }
    }
    #endregion

    #region actions
    public override void SwitchToGame()
    {
        MainController.HighscoreController.AddHighscoreEntry(inputField.text, MainController.HighscoreController.RecentScore);
        base.SwitchToGame();
    }

    public override void SwitchToMenu()
    {
        MainController.HighscoreController.AddHighscoreEntry(inputField.text, MainController.HighscoreController.RecentScore);
        base.SwitchToMenu();
    }
    #endregion

    #region destroy
    protected void OnDestroy()
    {
        if (highscoreEntryText != null)
        {
            highscoreEntryText = null;
        }

        if (scoreText != null)
        {
            scoreText = null;
        }

        if (inputField != null)
        {
            inputField = null;
        }
    }
    #endregion
}
