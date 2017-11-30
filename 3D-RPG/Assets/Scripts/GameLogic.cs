using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic{
	public static float ExperienceForNextLevel(int currentLevel)
	{
		if(currentLevel == 0)
		{
			return 0;
		}
		return(currentLevel * currentLevel + currentLevel + 3) * 4;//make getting exp harder and harder(exp curve)
	}

	public static float CalculatePlayerBaseAttackDamage(PlayerController playerController)
	{
		float attackDamage = playerController.strength + Mathf.Floor (playerController.strength / 10) * 3;
		return attackDamage;
	}

	public static float CalculatePlayerFireBallDamage(PlayerController playerController)
	{
		float FireDamage = playerController.Magic + Mathf.Floor (playerController.Magic / 10) * 3;
		return FireDamage;
	}
}
