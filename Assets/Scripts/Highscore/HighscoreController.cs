using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Represents a highscore entry
/// </summary>
[Serializable]
public class HighscoreEntry
{
    public string Name;
    public int Score;

    public HighscoreEntry(string name, int score)
    {
        this.Name = name;
        this.Score = score;
    }
}

/// <summary>
/// Manages the highscore list
/// </summary>
public class HighscoreController : MonoBehaviour
{
    private static HighscoreController _instance;
    private static string persistentDataName = "highscoreEntries.dat";

    private List<HighscoreEntry> highscoreEntries;

    #region setup
    protected void Start()
    {
        // Try to load existing highscores
        this.Load(out highscoreEntries);

        // highscoreEntries null? Might be not if a file could have been loaded.
        if (highscoreEntries == null)
        {
            highscoreEntries = new List<HighscoreEntry>();
            for (int i = 1; i <= 10; i++)
            {
                highscoreEntries.Add(new HighscoreEntry("Nummer " + i, 500 * i));
            }
        }
    }
    #endregion

    #region actions
    /// <summary>
    /// Create a new highscore entry
    /// </summary>
    /// <param name="name">The name of the player</param>
    /// <param name="score">The score the player achieved</param>
    public void AddHighscoreEntry(string name, int score)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry(name, score);
        highscoreEntries.Add(highscoreEntry);
        highscoreEntries = highscoreEntries.OrderBy(o => o.Score).ToList();
        this.Save();
    }

    /// <summary>
    /// Save the current highscore entries
    /// </summary>
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + persistentDataName);

        bf.Serialize(file, highscoreEntries);
        file.Close();
    }

    /// <summary>
    /// Load highscore entires
    /// </summary>
    /// <param name="highscoreEntries">The variable to save the loaded values to</param>
    public void Load(out List<HighscoreEntry> highscoreEntries)
    {
        if (File.Exists(Application.persistentDataPath + persistentDataName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + persistentDataName, FileMode.Open);

            highscoreEntries = (List<HighscoreEntry>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            highscoreEntries = null;
        }
    }
    #endregion

    #region debug
    /// <summary>
    /// Log the current highscore entries
    /// </summary>
    public void Print()
    {
        for (int i = 0; i < highscoreEntries.Count; i++)
        {
            HighscoreEntry highscoreEntry = highscoreEntries[i];
            Debug.Log(highscoreEntry.Name + ": " + highscoreEntry.Score + "\n");
        }
    }
    #endregion

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

    #region destroy
    protected void OnDestroy()
    {
        _instance = null;
    }
    #endregion
}
