using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour
{
    #region constants
    public static class SceneNames
    {
        public static readonly string MENU_SCENE = "Menu Scene";
        public static readonly string GAME_SCENE = "Game Scene";
        public static readonly string GAME_OVER_SCENE = "Game Over Scene";
    }

    public static class Tags
    {
        public static readonly string PLAYER = "Player";
        public static readonly string ENEMY = "Enemy";
        public static readonly string PLAYER_BULLET = "Player Bullet";
        public static readonly string ENEMY_BULLET = "Enemy Bullet";
    }
    #endregion

    #region further singletons
    public static HighscoreController highscoreController;
    public static ScoreController playerScore;
    #endregion

    #region variables
    private static MainController mainController;

    private string currentSceneName;
    private string nextSceneName;

    private AsyncOperation resourceUnloadTask;
    private AsyncOperation sceneLoadTask;

    private enum SceneState { Reset, Preload, Load, Unload, Postload, Ready, Run, Count };
    private SceneState sceneState;

    private delegate void UpdateDelegate();
    private UpdateDelegate[] updateDelegates;
    #endregion

    #region public static methods
    /// <summary>
    /// Switches to another scene
    /// </summary>
    /// <param name="newSceneName">The name of the new scene</param>
    public static void SwitchScene(string newSceneName)
    {
        if (mainController != null)
        {
            if (mainController.currentSceneName != newSceneName)
            {
                // By changing the scene name the update cycle starts
                mainController.nextSceneName = newSceneName;
            }
        }
    }
    #endregion

    #region Unity methods
    protected void Awake()
    {
        // Keep this gameobject alive
        Object.DontDestroyOnLoad(gameObject);

        // Setup singleton
        mainController = this;

        // Setup further singletons
        highscoreController = GetComponent<HighscoreController>();
        playerScore = GetComponent<ScoreController>();

        // Setup the array of updateDelegates
        updateDelegates = new UpdateDelegate[(int)SceneState.Count];

        // Set each updateDelegate
        updateDelegates[(int)SceneState.Reset] = UpdateSceneReset;
        updateDelegates[(int)SceneState.Preload] = UpdateScenePreload;
        updateDelegates[(int)SceneState.Load] = UpdateSceneLoad;
        updateDelegates[(int)SceneState.Unload] = UpdateSceneUnload;
        updateDelegates[(int)SceneState.Postload] = UpdateScenePostload;
        updateDelegates[(int)SceneState.Ready] = UpdateSceneReady;
        updateDelegates[(int)SceneState.Run] = UpdateSceneRun;

        // Switch to menu
        nextSceneName = MainController.SceneNames.MENU_SCENE;
        sceneState = SceneState.Reset;
    }

    protected void OnDestroy()
    {
        // Clean up updateDelegates
        if (updateDelegates != null)
        {
            for (int i = 0; i < (int)SceneState.Count; i++)
            {
                updateDelegates[i] = null;
            }
            updateDelegates = null;
        }

        // Clean up singleton
        if (mainController != null)
        {
            mainController = null;
        }
    }

    protected void OnDisable()
    {

    }

    protected void OnEnable()
    {

    }

    protected void Start()
    {

    }

    protected void Update()
    {
        if (updateDelegates[(int)sceneState] != null)
        {
            updateDelegates[(int)sceneState]();
        }
    }
    #endregion

    #region update delegates
    // Attach the new scene controller to start cascade of loading
    private void UpdateSceneReset()
    {
        // Run a GC pass
        System.GC.Collect();
        sceneState = SceneState.Preload;
    }

    // Handle anything that needs to happen before loading
    private void UpdateScenePreload()
    {
        sceneLoadTask = Application.LoadLevelAsync(nextSceneName);
        sceneState = SceneState.Load;
    }

    // Show loading screen until
    private void UpdateSceneLoad()
    {
        // Loading done?
        if (sceneLoadTask.isDone)
        {
            sceneState = SceneState.Unload;
        }
        else
        {
            // Show loading screen
        }
    }

    // Clean unused resources
    private void UpdateSceneUnload()
    {
        // Already cleaning up resources?
        if (resourceUnloadTask == null)
        {
            resourceUnloadTask = Resources.UnloadUnusedAssets();
        }
        else
        {
            // Cleaning done?
            if (resourceUnloadTask.isDone)
            {
                resourceUnloadTask = null;
                sceneState = SceneState.Postload;
            }
        }
    }

    // Anything directly after loading
    private void UpdateScenePostload()
    {
        currentSceneName = nextSceneName;
        sceneState = SceneState.Ready;
    }

    // Anything directly before running
    private void UpdateSceneReady()
    {
        // For instance a GC pass. I don't call it to prevent problems with object pool.
        sceneState = SceneState.Run;
    }

    // Wait for scene change
    private void UpdateSceneRun()
    {
        if (currentSceneName != nextSceneName)
        {
            // This will start the update cycle again
            sceneState = SceneState.Reset;
        }
    }
    #endregion
}
