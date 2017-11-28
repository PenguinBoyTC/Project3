using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
	public float experience;
    void Start () {
		
	}

   
    void Update () {
		
	}
	public void GetExperience(float exp){
		experience += exp;
	}
}
