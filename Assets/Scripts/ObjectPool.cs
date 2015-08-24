using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour 
{
    [SerializeField]
    private GameObject pooledGameObject;

    [SerializeField]
    private int poolCount = 20;

    private List<GameObject> gameObjectList;
    
    protected void Start()
    {
        // Setup the list for game objects
        gameObjectList = new List<GameObject>();

        // Instantiate a number of game objects specified by pool count
        for (int i = 0; i < poolCount; i++)
        {
            // Instantiate a new object
            GameObject obj = (GameObject)Instantiate(pooledGameObject, Vector3.zero, Quaternion.identity);

            // Deactivate the object for later use
            obj.SetActive(false);

            // Add the object to the list
            gameObjectList.Add(obj);
        }
    }

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
                pooledGameObject.SetActive(true);
                return gameObjectList[i];
            }
        }

        Debug.LogError("Object Pool empty. You should increase the pooled amount");

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
}
