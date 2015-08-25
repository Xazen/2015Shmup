using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Helper class that makes it easier to use it in the inspector
/// </summary>
[Serializable]
class Panner
{
    public float speedX = 1.0f;
    public float speedY = 1.0f;
}

/// <summary>
/// The texture panner is used to move textures continously
/// </summary>
public class TexturePanner : MonoBehaviour 
{
    [SerializeField]
    private Panner panner;

    private Material material = null;
    private float offsetX = 0;
    private float offsetY = 0;

    #region setup
    protected void Start()
    {
        // Get the material of the game object
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            material = rend.material;
        }
    }
    #endregion

    #region action
    // Update is called once per frame
	protected void Update () 
    {
        // Material available?
        if (material != null)
        {
            // Moves the textures
            offsetX = (offsetX + panner.speedX * Time.deltaTime) % 1.0f;
            offsetY = (offsetY + panner.speedY * Time.deltaTime) % 1.0f;
            material.mainTextureOffset = new Vector2(offsetX, offsetY);
        }
    }
    #endregion
}
