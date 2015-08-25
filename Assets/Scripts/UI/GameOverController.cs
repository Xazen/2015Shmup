using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Controls the game over screen
/// </summary>
public class GameOverController : MonoBehaviour 
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
        scoreText.text = MainController.PlayerScore.score.ToString();

        if (!MainController.HighscoreController.IsValid(MainController.PlayerScore.score))
        {
            highscoreEntryText.text = "Yeah! No Highscore!";
            inputField.placeholder.GetComponent<Text>().text = "You like to be in? Try it!";
        }
    }
    #endregion

    #region actions
    public void StartGame()
    {
        MainController.HighscoreController.AddHighscoreEntry(inputField.text, MainController.PlayerScore.score);
        MainController.SwitchScene(MainController.SceneNames.GAME_SCENE);
    }

    public void ReturnToMenu()
    {
        MainController.HighscoreController.AddHighscoreEntry(inputField.text, MainController.PlayerScore.score);
        MainController.SwitchScene(MainController.SceneNames.MENU_SCENE);
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
