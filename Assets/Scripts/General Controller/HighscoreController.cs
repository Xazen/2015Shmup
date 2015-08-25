using UnityEngine;
using System.Collections;

public class HighscoreController : MonoBehaviour
{
    private static HighscoreController _instance;

    #region Singleton
    public static HighscoreController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<HighscoreController>();
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
            DontDestroyOnLoad(this);
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
}
