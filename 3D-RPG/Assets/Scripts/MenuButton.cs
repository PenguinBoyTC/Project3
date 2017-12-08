using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {
	/**
    * Pre: the button transfer
    * Post: when the object is clicked, then transfer to the scene.
    * return: NA
    **/
	public void OnStartGame(string SceneName)
	{
		Application.LoadLevel (SceneName);
	}

}
