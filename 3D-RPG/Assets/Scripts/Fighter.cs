using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {
	public GameObject opponent;
	public AnimationClip attack;
	public AnimationClip dieClip;
	public int damage;
	public double impactTime;
	public bool impacted;
	public float range;
	public int health;
	private bool started;
	private bool ended;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(health);
		if (Input.GetKey(KeyCode.Space)&&inRange())
		{
			transform.GetComponent<Animation>().Play(attack.name);
			ClickToMove.attack = true;
			if(opponent!=null)
			{
				transform.LookAt(opponent.transform.position);
				
			}
		}
		if(GetComponent<Animation>()[attack.name].time>0.9*GetComponent<Animation>()[attack.name].length)
		{
			ClickToMove.attack = false;
			impacted =false;
		}
		impact();
		die();
	}
	
	void impact()
	{
		if(opponent!=null&&transform.GetComponent<Animation>().IsPlaying(attack.name)&&!impacted)
		{
			if((GetComponent<Animation>()[attack.name].time)>(GetComponent<Animation>()[attack.name].length*impactTime)&&GetComponent<Animation>()[attack.name].time<0.9*GetComponent<Animation>()[attack.name].length)
			{
				opponent.GetComponent<Mob>().getHit(damage);
				impacted = true;
			}
		}
	}
	
	public void getHit(int damage)
	{
		health = health - damage;
		if(health<0)
		{
			health = 0;	
		}
	}
	
	bool inRange()
	{
		if(Vector3.Distance(opponent.transform.position, transform.position)<=range)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	public bool isDead()
	{
		if(health == 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	void die()
	{
		if(isDead()&&!ended)
		{
			if(!started)
			{
				ClickToMove.die = true;
				GetComponent<Animation>().Play(dieClip.name);
				started = true;
			}	
			if(started&&!GetComponent<Animation>().IsPlaying(dieClip.name))
			{
				//What ever you want to do after died;
				Debug.Log("You have died");
				ended = true;
				started = false;
			}
		}
	}
}
