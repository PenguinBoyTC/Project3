using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {
	public void OnStartGame(string SceneName)
	{
		Application.LoadLevel (SceneName);
	}

}
