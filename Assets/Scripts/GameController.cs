using UnityEngine;
using System;
using System.Collections;

public class GameController : MonoBehaviour 
{
    public static InputController InputController;

	// Use this for initialization
	void Start () 
    {
        // Setup variables
        InputController = GetComponent<InputController>();
	}
}
