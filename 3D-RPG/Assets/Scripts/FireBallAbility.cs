using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAbility : MonoBehaviour {

	public Rigidbody FireBall;
	public Transform Muzzle;
	private bool start;
	public static float fireInterval;
	// Use this for initialization
	void Start () {
		start = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (AbilityControl1.AbilityActive && start) 
		{
			Rigidbody c = Rigidbody.Instantiate (FireBall, Muzzle.position, Muzzle.rotation)
			as Rigidbody;
			c.AddForce (Muzzle.forward * 1250);

			StartCoroutine (FireInterval());
		}	
	}
	IEnumerator FireInterval(){
		start = false;
		yield return new WaitForSeconds (1/fireInterval);
		start = true;
	}
}
