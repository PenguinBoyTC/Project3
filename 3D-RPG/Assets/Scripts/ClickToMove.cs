using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMove : MonoBehaviour {
	public float speed;
	public CharacterController controller;
	private Vector3 position;
	//public AnimationClip run;
	//public AnimationClip idle;
	//public static bool attack;
	//public static bool die;
	public Animator anim;


	// Use this for initialization
	void Start () {
		position = transform.position;	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(1))
		{
            Debug.Log("Clicking");
			locatePosition();			
		}
		moveToPosition();
	}
	void locatePosition()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit, 1000))
		{
			position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
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
}
