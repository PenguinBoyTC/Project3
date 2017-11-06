using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
	NavMeshAgent nav;
	public float speed;
	private Vector3 position;

    /**
    * Pre: PlayerController object created
    * Post: PlayerController object initialized
    * return: NA
    **/
    void Start () {
		position = transform.position;	
		nav = GetComponent<NavMeshAgent> ();
	}

    /**
    * Pre: PlayerController object initialized
    * Post: PlayerController object updated for that frame
    * return: NA
    **/
    void Update () {
		if (Input.GetMouseButtonDown (0)) {
			locatePosition ();
			//Create a relation between the map and player so that the player can check where can go.
			if (Vector3.Distance (transform.position, position) > 1) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 100)) {
					nav.SetDestination (hit.point);
				}
			}
		}
	}

    /**
    * Pre: PlayerController object created
    * Post: PlayerController finds position of mouse for attacking
    * return: NA
    **/
    void locatePosition()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit, 1000))
		{
			//if(hit.collider.tag!="Player"&&hit.collider.tag!="Enemy")
			//{
			position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
			Debug.Log (position);
			//}
		}
	}
}
