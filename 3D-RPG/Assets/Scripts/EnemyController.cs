using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	private bool dead;
	public float totalHealth;
	public float currentHealth;
	public float expGranted;
	public float attackDamage;
	public float dieAftertime;
	// Use this for initialization
	void Start () {
		currentHealth = totalHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void GetHit(float damage){
		if (dead) {
			return;
		}
		currentHealth -= damage;
		if (currentHealth <= 0) {
			Die ();
		}
	}
	void Die()
	{
		dead = true;
		GameObject.Destroy (this.gameObject, dieAftertime);
	}
	void DropLoot(){
		print ("You get the bounty");
	}
}
