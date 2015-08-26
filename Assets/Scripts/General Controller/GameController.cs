using UnityEngine;
using System;
using System.Collections;

#region border implementation
/// <summary>
/// Border class is used to determine a border. e.g. game viewport.
/// </summary>
[Serializable]
public struct Border
{
    public float top;
    public float left;
    public float bottom;
    public float right;

    public Border(float up, float left, float down, float right)
    {
        this.top = up;
        this.left = left;
        this.bottom = down;
        this.right = right;
    }

    public void Print()
    {
        Debug.Log("Border: " + this + "\n" +
            "top    : " + this.top + "\n" +
            "left   : " + this.left + "\n" +
            "bottom : " + this.bottom + "\n" +
            "right  : " + this.right);
    }
}
#endregion

/// <summary>
/// Manages all game objects that need to be accessible by various different scripts
/// </summary>
public class GameController : MonoBehaviour 
{
    public InputController inputController;
    public GameObject player;
    [SerializeField]
    private GameObject pauseScreen;
    
    [HideInInspector]
    public Border gameArea;

    private static GameController _instance;

    private float pausedTimeScale = 1.0f;

    #region Singleton
    public static GameController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameController>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            // Make it a Singleton if this is the first instance
            _instance = this;
        }
        else
        {
            // In case there is another instance, destroy it
            if (this != _instance)
            {
                Destroy(this.gameObject);
            }
        }
    }
    #endregion

    #region setup
    // Use this for initialization
	void Start () 
    {
        // Setup player health delegate
        player.GetComponent<PlayerHealth>().healthDepletedEvent += OnHealthDepleted;

        // Setup variables
        this.inputController = this.GetComponent<InputController>();

        // Get reference points to setup game area
        Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.y));
        Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.y));

        // Setup game area
        gameArea = new Border(
            upperRight.z,
            lowerLeft.x,
            lowerLeft.z,
            upperRight.x
            );
	}
    #endregion

    #region actions
    void Update()
    {
        // Trigger pause when paused is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TriggerPause();
        }
    }

    /// <summary>
    /// Pause and resume the game
    /// </summary>
    public void TriggerPause()
    {
        // Pause the game
        if (Time.timeScale > 0)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }

    /// <summary>
    /// Pause the game
    /// </summary>
    public void Pause()
    {
        // Keep the current time scale
        pausedTimeScale = Time.timeScale;
        Time.timeScale = 0;

        pauseScreen.SetActive(true);
    }

    /// <summary>
    /// Unpause the game
    /// </summary>
    public void Unpause()
    {
        // Return to previous time scale
        Time.timeScale = pausedTimeScale;

        pauseScreen.SetActive(false);
    }

    /// <summary>
    /// Show game over screen
    /// </summary>
    public void ShowGameOver()
    {
        MainController.SwitchScene(MainController.SceneNames.GAME_OVER_SCENE);
    }
    #endregion

    #region events
    /// <summary>
    /// Called when the health of the player is empty
    /// </summary>
    public void OnHealthDepleted()
    {
        // Show game over screen
        if (MainController.HighscoreController != null)
        {
            MainController.HighscoreController.RecentScore = player.GetComponent<PlayerScore>().Score;
        }
        else
        {
            Debug.LogError("Please start the game from the Main Scene.", this);
        }
        ShowGameOver();
    }
    #endregion

    #region destroy
    protected void OnDestroy()
    {
        if (inputController != null)
        {
            inputController = null;
        }

        if (player != null)
        {
            player = null;
        }

        _instance = null;
    }
    #endregion
}
