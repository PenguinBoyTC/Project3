using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic{
	/**
    * Pre: Experience logic
    * Post: Calculate the total experience for next level due to the current level
    * return: return the needed total Experience
    **/
	public static float ExperienceForNextLevel(int currentLevel)
	{
		if(currentLevel == 0)
		{
			return 0;
		}
		return(currentLevel * currentLevel + currentLevel + 3) * 4;//make getting exp harder and harder(exp curve)
	}
	/**
    * Pre: Normal attack damage logic
    * Post: Calculate attack damage for each level due to the a damage curve.
    * return: return damage value.
    **/
	public static float CalculatePlayerBaseAttackDamage(PlayerController playerController)
	{
		float attackDamage = playerController.strength + Mathf.Floor (playerController.strength / 10) * 3;
		return attackDamage;
	}
	/**
    * Pre: fireball damage logic
    * Post: Calculate each fireball damage for each level due to the a damage curve.
    * return: return damage value.
    **/
	public static float CalculatePlayerFireBallDamage(PlayerController playerController)
	{
		float FireDamage = playerController.Magic + Mathf.Floor (playerController.Magic / 10) * 3;
		return FireDamage;
	}
}
