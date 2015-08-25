using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour 
{
    public void StartGame()
    {
        MainController.SwitchScene(MainController.SceneNames.GAME_SCENE);
    }
}
