using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Manages the menu screen
/// </summary>
public class MenuSceneController : SceneController 
{
    [SerializeField]
    private Text highscoreUI;

    protected void Awake()
    {
        highscoreUI.text = MainController.HighscoreController.highscoreEntries[0].Score.ToString();
    }
}
