using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour 
{
    public void StartGame()
    {
        MainController.SwitchScene(MainController.SceneNames.GAME_SCENE);
    }
}
