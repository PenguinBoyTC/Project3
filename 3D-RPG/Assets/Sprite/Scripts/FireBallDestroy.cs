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
	void GetEnemiesInRange(){
		enemiesInRange.Clear ();
		foreach (Collider c in Physics.OverlapSphere ((transform.position + transform.forward*fireRange),fireRange)) {//the best attackRange=1.5f
			if (c.gameObject.CompareTag ("enemy")) {
				enemiesInRange.Add (c.transform);
			}
		}
	}
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
