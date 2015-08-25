using UnityEngine;
using System.Collections;

/// <summary>
/// Manages the menu screen
/// </summary>
public class MenuController : MonoBehaviour 
{
    public void StartGame()
    {
        MainController.SwitchScene(MainController.SceneNames.GAME_SCENE);
    }
}
