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

public class GameController : MonoBehaviour 
{
    public static InputController InputController;
    public static Border GameArea;

    private float pausedTimeScale;

	// Use this for initialization
	void Start () 
    {
        // Setup variables
        InputController = GetComponent<InputController>();

        // Get reference points to setup game area
        Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.y));
        Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.y));

        // Setup game area
        GameArea = new Border(
            upperRight.z,
            lowerLeft.x,
            lowerLeft.z,
            upperRight.x
            );
	}

    public void TriggerPause()
    {
        if (Time.timeScale > 0)
        {
            pausedTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = pausedTimeScale;
        }
        
    }
}
