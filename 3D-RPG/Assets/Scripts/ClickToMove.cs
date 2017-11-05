using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour {
	public float speed;
	public float attacktime;
	public CharacterController controller;
	private Vector3 position;
	public static bool attack;
	public static bool die;
	public Animator anim;

	private List<Transform> enemiesInRange = new List<Transform> ();
	private bool canMove;
	private bool canAttack;
	public float attackDamage;
	public float attackSpeed;
	public float attackRange;


	// Use this for initialization
	void Start () {
		position = transform.position;	
		canMove = true;
		canAttack = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			Debug.Log ("Attacking");
			Attack ();
			position = transform.position;	
		}
		else if (canMove) {
			if (Input.GetMouseButton (1)) {
				Debug.Log ("Clicking");
				locatePosition ();			
			}
			moveToPosition ();
		} 

	}
	void locatePosition()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit, 1000))
		{
			if (hit.collider.tag != "Player" && hit.collider.tag != "enemy") 
			{
				position = new Vector3 (hit.point.x, hit.point.y, hit.point.z);
			}
		}
	}
	void moveToPosition()
	{
		//Game Object is moving;
		if (Vector3.Distance (transform.position, position) > 1) {
			Quaternion newRotation = Quaternion.LookRotation (position - transform.position, Vector3.forward);
			newRotation.x = 0f;
			newRotation.z = 0f;
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 10);
			controller.SimpleMove (transform.forward * speed);
			anim.SetInteger ("Condition", 1);
		}
		else {
			anim.SetInteger ("Condition", 0);
		}
	}
	void Attack(){
		if (!canAttack) {
			return;
		}
		anim.SetInteger ("Condition", 2);
		StartCoroutine (AttackRoutine ());
		StartCoroutine (AttackCooldown ());

	}
	IEnumerator AttackRoutine()
	{
		canMove = false;
		yield return new WaitForSeconds (0.1f);
		anim.SetInteger ("Condition", 0);
		GetEnemiesInRange ();
		foreach (Transform enemy in enemiesInRange) {
			EnemyController ec = enemy.GetComponent<EnemyController> ();
			if (ec == null) {
				continue;
			}
			ec.GetHit (attackDamage);
		}
		yield return new WaitForSeconds (0.60f);
		canMove = true;
	}
	IEnumerator AttackCooldown(){
		canAttack = false;
		yield return new WaitForSeconds (1 / attackSpeed);
		canAttack = true;
	}
	void GetEnemiesInRange(){
		foreach (Collider c in Physics.OverlapSphere ((transform.position + transform.forward*0.5f),0.5f)) {
			if (c.gameObject.CompareTag ("enemy")) {
				enemiesInRange.Add (c.transform);
			}
		}
	}
}
