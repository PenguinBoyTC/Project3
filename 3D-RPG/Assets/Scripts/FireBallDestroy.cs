using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallDestroy : MonoBehaviour {
	public static float firelong;
	public static float firedamage;
	public static float fireRange;
	private List<Transform> enemiesInRange = new List<Transform> ();
	private bool isHit;
	// Use this for initialization
	void Start () {
		isHit = false;
		GameObject.Destroy (gameObject, firelong);
		//AnimationEvents.OnSlashAnimationHit += DealDamage;
	}
	void Update () {
		if (!isHit) 
		{
			DealDamage ();
		}
	}
	/**
    * Pre: If enemies is in range.
    * Post: Calculate the distance between enemy and fireball object and if the distance is less than fireRange than set to list
    * return: NA
    **/
	void GetEnemiesInRange(){
		enemiesInRange.Clear ();
		foreach (Collider c in Physics.OverlapSphere ((transform.position + transform.forward*fireRange),fireRange)) {//the best attackRange=1.5f
			if (c.gameObject.CompareTag ("enemy")) {
				enemiesInRange.Add (c.transform);
			}
		}
	}
	/**
    * Pre: get the damage.
    * Post: cost damage when enemy is in the range,
    * return: NA
    **/
	void DealDamage(){
		GetEnemiesInRange ();
		foreach (Transform enemy in enemiesInRange) {
			EnemyController ec = enemy.GetComponent<EnemyController> ();
			if (ec == null) {
				continue;
			}
			ec.GetHit (firedamage);
			isHit = true;
			GameObject.Destroy (gameObject, 0);
            print("enemy destroyed");
		}
	}
}
