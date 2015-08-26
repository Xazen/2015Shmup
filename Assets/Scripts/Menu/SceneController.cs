using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour 
{
    public virtual void SwitchToMenu()
    {
        MainController.SwitchScene(MainController.SceneNames.MENU_SCENE);
    }
    
    public virtual void SwitchToGame()
    {
        MainController.SwitchScene(MainController.SceneNames.GAME_SCENE);
    }

    public virtual void SwitchToHighscore()
    {
        MainController.SwitchScene(MainController.SceneNames.HIGHSCORE_SCENE);
    }

    public virtual void SwitchToGameOver()
    {
        MainController.SwitchScene(MainController.SceneNames.GAME_OVER_SCENE);
    }
}
