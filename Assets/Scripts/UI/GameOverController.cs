using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the game over screen
/// </summary>
public class GameOverController : MonoBehaviour 
{
    public void StartGame()
    {
        MainController.SwitchScene(MainController.SceneNames.GAME_SCENE);
    }
}
