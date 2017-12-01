using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	private bool dead;
	public float totalHealth;
	public float currentHealth;
	public float expGranted;
	//public float attackDamage;
	public float dieAftertime;

	private GameObject[] players;

    /**
    * Pre: EnemyController object created
    * Post: EnemyController object initialized
    * return: NA
    **/
    void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");
		currentHealth = totalHealth;
	}

    /**
    * Pre: EnemyController object initialized and hit
    * Post: EnemyController health adjusted
    * return: NA
    **/
    public void GetHit(float damage)
	{
		if (dead) {
			return;
		}
		currentHealth -= damage;
		if (currentHealth <= 0) {
			Die ();
			return;
		}
		//StartCoroutine (RecoverFromHit ());
	}

    /**
    * Pre: EnemyController object has no health
    * Post: Enemy object destroyed
    * return: NA
    **/
    void Die()//Destroy the enemy object.
	{
		dead = true;
		//animation.SetInteger ("Condition",4);
		DropLoot();
		foreach (GameObject go in players) {
			go.GetComponent<PlayerController>().SetExperience (expGranted/players.Length);
		}
		GameObject.Destroy (this.gameObject, dieAftertime);
	}

    /**
    * Pre: Enemy object destroyed
    * Post: Message printed
    * return: NA
    **/
    void DropLoot(){
		print ("You get the bounty");
	}
}
