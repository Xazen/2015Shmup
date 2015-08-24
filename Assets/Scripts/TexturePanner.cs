using UnityEngine;
using System;
using System.Collections;

[Serializable]
class Panner
{
    public float speedX = 1.0f;
    public float speedY = 1.0f;
}

public class TexturePanner : MonoBehaviour 
{
    [SerializeField]
    private Panner panner;

    private Material material = null;
    private float offsetX = 0;
    private float offsetY = 0;

    protected void Start()
    {
        // Get the material of the game object
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            material = rend.material;
        }
    }

	// Update is called once per frame
	protected void Update () 
    {
        if (material != null)
        {
            offsetX = (offsetX + panner.speedX * Time.deltaTime) % 1.0f;
            offsetY = (offsetY + panner.speedY * Time.deltaTime) % 1.0f;
            material.mainTextureOffset = new Vector2(offsetX, offsetY);
        }
	}
}
