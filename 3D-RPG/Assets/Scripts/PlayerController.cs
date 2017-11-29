using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
	
	public float experience{ get; private set;}
	private int level;
	private Text levelText;
	private Text HP_Text;
	private Transform experienceBar;
    private Transform healthBar;
	public float totalHealth;
	public float currentHealth;
	public float strength{get; private set;}
	public float bonusDamage;
	public float attackDamage;
	public float attackRange;
	private List<Transform> enemiesInRange = new List<Transform> ();
    private bool alive = true;

    void Start () {
		level = 1;
		strength = 1;
		currentHealth = totalHealth;
		AnimationEvents.OnSlashAnimationHit += DealDamage;
		experienceBar = UIController.instance.transform.Find ("Background/Experience");
        healthBar = UIController.instance.transform.Find("Background/Health");
		levelText = UIController.instance.transform.Find ("Background/Level_Text").GetComponent<Text> ();
		HP_Text = UIController.instance.transform.Find ("Background/Health/HP_Text").GetComponent<Text> ();
		SetAttackDamage ();
		SetExperience (0);
	}

   
    void Update () {
		
	}
	public void SetExperience(float exp){
		experience += exp;
		float experienceNeeded = GameLogic.ExperienceForNextLevel (level);
		float previousExperience = GameLogic.ExperienceForNextLevel (level - 1);
		while (experience >= experienceNeeded) {
			LevelUp ();
			experienceNeeded= GameLogic.ExperienceForNextLevel (level);
			previousExperience = GameLogic.ExperienceForNextLevel (level - 1);
		}
		experienceBar.Find ("Fill_bar").GetComponent<Image> ().fillAmount = (experience - previousExperience) / (experienceNeeded - previousExperience);
	}
	void LevelUp(){
		level++;
		strength++;
		SetAttackDamage ();
		levelText.text = "LV." + level.ToString ("00");
	}

	public void GetHit(float damage)
    {
		if (currentHealth <= 0) {
			currentHealth = 0;
			alive = false;
			print ("player out of health");
		} 
		else 
		{
			currentHealth -= damage;
			healthBar.Find("Fill_bar").GetComponent<Image>().fillAmount = currentHealth / totalHealth;
			HP_Text.text = "HP: " + currentHealth.ToString () + "/" + totalHealth.ToString();
		}
    }
	/**
    * Pre: player object created
    * Post: enemy object within range is set to able to be attacked
    * return: NA
    **/
	void GetEnemiesInRange(){
		enemiesInRange.Clear ();
		foreach (Collider c in Physics.OverlapSphere ((transform.position + transform.forward*attackRange),attackRange)) {//the best attackRange=1.5f
			if (c.gameObject.CompareTag ("enemy")) {
				enemiesInRange.Add (c.transform);
			}
		}
	}
	void DealDamage(){
		GetEnemiesInRange ();
		print("deal damage");
		foreach (Transform enemy in enemiesInRange) {
			EnemyController ec = enemy.GetComponent<EnemyController> ();
			if (ec == null) {
				continue;
			}
			ec.GetHit (attackDamage);
		}
	}
	void SetAttackDamage(){
		attackDamage = GameLogic.CalculatePlayerBaseAttackDamage(this) + bonusDamage;
	}
}
