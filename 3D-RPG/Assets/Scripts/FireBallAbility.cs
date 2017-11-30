using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAbility : MonoBehaviour {

	public Rigidbody FireBall;
	public Transform Muzzle;
	public static

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (AbilityControl1.AbilityActive) 
		{
			Rigidbody c = Rigidbody.Instantiate (FireBall, Muzzle.position, Muzzle.rotation)
			as Rigidbody;
			c.AddForce (Muzzle.forward * 1250);
		}	
	}
}
