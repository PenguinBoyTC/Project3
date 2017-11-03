using UnityEngine;

public class waypoints : MonoBehaviour {

	public static Transform[] points;	//list of game objects

	void Awake()
	{
		points = new Transform[transform.childCount];  //creates array for waypoints for enemy movement (15 currently)
		for(int i = 0; i < points.Length; i++)
		{
			points[i] = transform.GetChild(i);  //sets array points to the waypoints through getting children of points
		}
	}
}
