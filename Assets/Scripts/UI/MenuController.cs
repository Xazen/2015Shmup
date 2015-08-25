using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Manages the menu screen
/// </summary>
public class MenuController : MonoBehaviour 
{
    [SerializeField]
    private Text highscoreUI;

    protected void Awake()
    {
        highscoreUI.text = MainController.HighscoreController.highscoreEntries[0].Score.ToString();
    }

    public void StartGame()
    {
        MainController.SwitchScene(MainController.SceneNames.GAME_SCENE);
    }

    public void OpenHighscoreScene()
    {
        MainController.SwitchScene(MainController.SceneNames.HIGHSCORE_SCENE);
    }
}
