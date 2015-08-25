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

    public void Awake()
    {
        // Insert the score of the player to the text
        scoreText.text = MainController.PlayerScore.score.ToString();

        if (!MainController.HighscoreController.IsValid(MainController.PlayerScore.score))
        {
            highscoreEntryText.text = "No Highscore!";
            inputField.placeholder.GetComponent<Text>().text = "You like to be in? Try it!";
        }
    }

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
}
