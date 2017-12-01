using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class enemy_movement : MonoBehaviour
{

    public float enemy_speed = 10f;
	public float attackDamage;
    private Transform target;
    private int wavepiont_index = 0;

    private Text enemy_death_test_text;

    public int testing = 0;

    private GameObject[] players;

    /**
    * Pre: enemy object created
    * Post: enemy object initialized by setting target to the first waypoint
    * return: NA
    **/
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        target = waypoints.points[waypoints.points.Length - 1];  //starts at point 0, inside start/spawn
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

            if (testing != 0)
            {
                enemy_death_test_text = UIController.instance.transform.Find("Tests/Result (4)").GetComponent<Text>();
                enemy_death_test_text.text = "Passed";
            }
            Destroy(gameObject);
            print("enemy reached base");
            return;
        }
    }

}
