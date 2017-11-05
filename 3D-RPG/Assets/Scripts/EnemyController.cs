using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	public float totalHealth;
	public float currentHealth;
	// Use this for initialization
	void Start () {
		currentHealth = totalHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void GetHit(float damage){
		currentHealth -= damage;
	}
}
