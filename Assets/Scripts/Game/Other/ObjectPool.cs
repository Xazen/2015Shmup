﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class manages multiple objects of a gameobject to avoid runtime allocations.
/// </summary>
public class ObjectPool : MonoBehaviour 
{
    public GameObject pooledGameObject;

    [SerializeField]
    private int poolCount;

    private List<GameObject> gameObjectList;

    #region setup
    protected void Start()
    {
        // Check if the required values are set
        if (pooledGameObject != null)
        {
            Initialize();
        }
    }

    /// <summary>
    /// Initialize the actual object pool
    /// </summary>
    public void Initialize()
    {
        // Create an empty game object as a folder
        GameObject parentGameObject = new GameObject();
        parentGameObject.name = pooledGameObject.name;

        // Setup the list for game objects
        gameObjectList = new List<GameObject>();

        // Instantiate a number of game objects specified by pool count
        for (int i = 0; i < poolCount; i++)
        {
            // Instantiate a new object
            GameObject obj = (GameObject)Instantiate(pooledGameObject, Vector3.zero, Quaternion.identity);

            // Deactivate the object for later use
            obj.SetActive(false);

            // Add the object to the list and the parent game object
            gameObjectList.Add(obj);
            obj.transform.SetParent(parentGameObject.transform);
        }
    }
    #endregion

    #region actions
    /// <summary>
    /// Return a game object from the pool. When there are no more left null is returned.
    /// </summary>
    /// <returns>A game object</returns>
    public GameObject GetGameObject()
    {
        for (int i = 0; i < poolCount; i++)
        {
            if (!gameObjectList[i].activeSelf)
            {
                gameObjectList[i].SetActive(true);
                return gameObjectList[i];
            }
        }

        Debug.LogError("Object Pool empty for " + pooledGameObject.name + ". You should increase the pooled amount");

        return null;
    }

    /// <summary>
    /// Return game object back to the object pool
    /// </summary>
    /// <param name="gameObject">The game object to return</param>
    public void ReturnGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region destroy
    protected void OnDestroy()
    {
        if (gameObjectList != null && gameObjectList.Count > 0)
        {
            for (int i = 0; i < gameObjectList.Count; i++)
            {
                if (gameObjectList[i] != null)
                {
                    if (gameObjectList[i].activeSelf)
                    {
                        gameObjectList[i].SetActive(false);
                    }

                    gameObjectList[i] = null;
                }
            }

            gameObjectList = null;
        }

        if (pooledGameObject != null)
        {
            pooledGameObject = null;
        }
    }
    #endregion
}
