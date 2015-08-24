using UnityEngine;
using System;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public static InputController InputController;

	// Use this for initialization
	void Start () 
    {
        Debug.Log("start game controller");
        InputController = GetComponent<InputController>();
        if (InputController == null)
        {
            Debug.LogError("No InputController found in " + this.gameObject.name + ". Add an InputController script as component.");
        }
	}
}
