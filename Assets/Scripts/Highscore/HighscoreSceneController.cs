using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighscoreSceneController : MonoBehaviour 
{
    [SerializeField]
    private GameObject highscoreTable;

    [SerializeField]
    private int maxNameLengt = 12;

    protected void Start()
    {
        for (int i = 0; i < highscoreTable.transform.childCount; i++)
        {
            // Get the row
            GameObject row = highscoreTable.transform.GetChild(i).gameObject;

            // Get high score entry
            HighscoreEntry highscoreEntry = MainController.HighscoreController.highscoreEntries[i];

            // Assign to list
            Text nameText = row.transform.GetChild(1).gameObject.GetComponent<Text>();

            // Set the name with max 10 characters
            nameText.text = highscoreEntry.Name.Substring(0, Mathf.Min(highscoreEntry.Name.Length, maxNameLengt));

            Text scoreText = row.transform.GetChild(2).gameObject.GetComponent<Text>();
            scoreText.text = highscoreEntry.Score.ToString();
        }
    }

    #region actions
    public void ReturnToMenu()
    {
        MainController.SwitchScene(MainController.SceneNames.MENU_SCENE);
    }
    #endregion

    #region destroy
    protected void OnDestroy()
    {
        if (highscoreTable != null)
        {
            highscoreTable = null;
        }
    }
    #endregion
}
