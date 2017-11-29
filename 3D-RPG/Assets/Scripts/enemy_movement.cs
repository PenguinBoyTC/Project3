using UnityEngine;

public class enemy_movement : MonoBehaviour
{

    public float enemy_speed = 10f;
	public float attackDamage;
    private Transform target;
    private int wavepiont_index = 0;

    private GameObject[] players;

    /**
    * Pre: enemy object created
    * Post: enemy object initialized by setting target to the first waypoint
    * return: NA
    **/
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        target = waypoints.points[8];  //starts at point 0, inside start/spawn
    }

    /**
    * Pre: enemy object initialized and has target
    * Post: enemy moves towards waypoint
    * return: NA
    **/
    void Update()
    {
        Vector3 dir = target.position - transform.position;  //move in direction on next wavepoint via subtracting current from next
        transform.Translate(dir.normalized * enemy_speed * Time.deltaTime, Space.World);  //fixed speed, deltaTime makes sure speed doesn't depend on framerate

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            foreach (GameObject go in players)
            {
				go.GetComponent<PlayerController>().GetHit(attackDamage);
            }
            Destroy(gameObject);
            print("enemy reached base");
            return;
        }
    }

}
