using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour {
	public float speed;
	public float range;
	public int health;
	public int damage;
	
	public CharacterController controller;
	public Transform player;
	
	public AnimationClip run;
	public AnimationClip idle;
	public AnimationClip attackClip;
	public AnimationClip die;
	
	
	public double impactTime;
	private bool impacted;

	// Use this for initialization
	void Start () {
		//health = 100;
		//opponent = player.GetComponent<Fighter>();//it's the same as Fighter opponent;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isDead())
		{
			if(!inRange())
			{
				chase();
			}
			else
			{
				//transform.GetComponent<Animation>().CrossFade(idle.name);
				transform.GetComponent<Animation>().Play(attackClip.name);
				attack();
				if(GetComponent<Animation>()[attackClip.name].time>GetComponent<Animation>()[attackClip.name].length*0.9)
				{
					impacted = false;
				}
			}
		}
		else
		{
			dieMethod();
		}
		
	}
	void attack()
	{
		if(GetComponent<Animation>()[attackClip.name].time>GetComponent<Animation>()[attackClip.name].length*impactTime&&!impacted&& GetComponent<Animation>()[attackClip.name].time<GetComponent<Animation>()[attackClip.name].length*0.9)
		{
			player.GetComponent<Fighter>().getHit(damage);
			impacted = true;
		}
	}
	
	bool inRange()
	{
		if (Vector3.Distance(transform.position, player.position)<range)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	public void getHit(int damage)
	{
		health = health - damage;
		if(health<0)
		{
			health=0;	
		}
	}
	
	void chase()
	{
		transform.LookAt(player.position);
		controller.SimpleMove(transform.forward*speed);
		transform.GetComponent<Animation>().CrossFade(run.name);
	}
	void OnMouseOver()
	{
		player.GetComponent<Fighter>().opponent = gameObject;
	}
	bool isDead()
	{
		if(health<=0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	void dieMethod()
	{
		transform.GetComponent<Animation>().Play(die.name);
		if((GetComponent<Animation>()[die.name].time)>(0.9*GetComponent<Animation>()[die.name].length))
		{
			Destroy(gameObject);
		}
	}
	
}
