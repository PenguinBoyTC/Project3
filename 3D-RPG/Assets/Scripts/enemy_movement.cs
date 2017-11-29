using UnityEngine;

public class enemy_movement : MonoBehaviour
{

    public float enemy_speed = 10f;

    private Transform target;
    private int wavepiont_index = 0;

    /**
    * Pre: enemy object created
    * Post: enemy object initialized by setting target to the first waypoint
    * return: NA
    **/
    void Start()
    {
        target = waypoints.points[0];  //starts at point 0, inside start/spawn
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
            Get_next_waypoint();  //reached a waypoint, get the next
        }
    }

    /**
    * Pre: enemy object created
    * Post: enemy target waypoint set to next waypoint in the waypoint array
    * return: NA
    **/
    void Get_next_waypoint()
    {
        if (wavepiont_index >= waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        wavepiont_index++;  //increase index to find next point
        target = waypoints.points[wavepiont_index];  //sets new target to the next waypoint within the array
    }

}
