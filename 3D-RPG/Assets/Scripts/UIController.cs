using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
	public static UIController instance;
	// Use this for initialization
	/**
    * Pre: set to instance
    * Post: Set a instance property for each bar at the beginning
    * return: NA
    **/
	void Start () {
		if (!instance) {
			instance = this;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
