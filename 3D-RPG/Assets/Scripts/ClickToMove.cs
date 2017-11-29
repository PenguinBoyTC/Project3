using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour {
	public float speed;
	public CharacterController controller;
	private Vector3 position;
	public static bool attack;
	public static bool die;
	public Animator anim;

	private bool canMove;
	private bool canAttack;
	public float attackSpeed;


    /**
    * Pre: player object created
    * Post: player object initialized to allow for movement and attack
    * return: NA
    **/
    void Start () {
		position = transform.position;	
		canMove = true;
		canAttack = true;
	}

    /**
    * Pre: player object created
    * Post: methods called as based on input of mouse or keyboard
    * return: NA
    **/
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

    /**
    * Pre: player object created
    * Post: position of enemy determined for attack
    * return: NA
    **/
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

    /**
    * Pre: player object created
    * Post: player moved to position of click
    * return: NA
    **/
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

    /**
    * Pre: player object created and attack key is pressed
    * Post: animation of attacking called
    * return: NA
    **/
    void Attack(){
		if (!canAttack) {
			return;
		}
		anim.speed = attackSpeed;
		anim.SetInteger ("Condition", 2);
		StartCoroutine (AttackRoutine ());
		StartCoroutine (AttackCooldown ());

	}

    /**
    * Pre: player object created and attack animation called
    * Post: attack animation commences
    * return: NA
    **/
    IEnumerator AttackRoutine()
	{
		canMove = false;
		yield return new WaitForSeconds (0.1f);
		anim.SetInteger ("Condition", 0);
	
		yield return new WaitForSeconds (1/attackSpeed);
		canMove = true;
	}

    /**
    * Pre: player object created
    * Post: attack animation cooldown set/adjusted
    * return: NA
    **/
    IEnumerator AttackCooldown(){
		canAttack = false;
		yield return new WaitForSeconds (1 / attackSpeed);
		canAttack = true;
	}
}
