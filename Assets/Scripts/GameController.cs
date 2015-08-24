using UnityEngine;
using System;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public InputController inputController;

	// Use this for initialization
	void Start () 
    {
        inputController = GetComponent<InputController>();
        if (inputController == null)
        {
            Debug.LogError("No InputController found in " + this.gameObject.name + ". Add an InputController script as component.");
        }
	}
}
