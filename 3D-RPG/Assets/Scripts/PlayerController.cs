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
	private Text MP_Text;
	private Transform experienceBar;
    private Transform healthBar;

    private Text health_test_text;

	private Transform abilityBar;


	public float totalHealth;
	public float currentHealth;

	public float strength{get; private set;}
	public float bonusDamage;
	public float attackDamage;
	public float attackRange;

	public float Magic{get; private set;}
	public float FireBallDamage;
	public float totalMagic;
	public float currentMagic;

    public int testing = 0;
	private bool istesting;
    public Text get_hit_test_text;


	private List<Transform> enemiesInRange = new List<Transform> ();
    private bool alive;
	public static bool isMPEmpty;
	//initial the player at the beginning.
    void Start () {
		istesting = false;
		alive = true;
		level = 1;
		strength = 1;
		currentHealth = totalHealth;
		currentMagic = totalMagic;
		isMPEmpty = false;
		AnimationEvents.OnSlashAnimationHit += DealDamage;

		experienceBar = UIController.instance.transform.Find ("Background/Experience");
        healthBar = UIController.instance.transform.Find("Background/Health");
		abilityBar = UIController.instance.transform.Find("Background/Ability");

		levelText = UIController.instance.transform.Find ("Background/Level_Text").GetComponent<Text> ();
		HP_Text = UIController.instance.transform.Find ("Background/Health/HP_Text").GetComponent<Text> ();
		MP_Text = UIController.instance.transform.Find ("Background/Ability/MP_Text").GetComponent<Text> ();

		SetAttackDamage ();
		SetFireBallDamage ();
		SetExperience (0);

        if (currentHealth == totalHealth) //test0 here 
        {
            health_test_text = UIController.instance.transform.Find("Tests/Result").GetComponent<Text>();
            health_test_text.text = "Passed";
        }

        if (testing != 0)
        {
			istesting = true;
            test_GetHit();
        }
	}

   
    void Update () {
		if (alive) 
		{
			if (AbilityControl1.isMagicCost) 
			{
				UseFireBall ();
				AbilityControl1.isMagicCost = false;
			}
		} 
		else if(!alive&&!istesting)
		{
			Application.LoadLevel ("GameOver");
		}
		
	}
	/**
    * Pre: Set experience value
    * Post: A way to set experience value
    * return: NA
    **/
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
	/**
    * Pre: Level Up controller
    * Post: set the level up condition for player
    * return: NA
    **/
	void LevelUp(){
		
		level++;
		strength++;
		SetAttackDamage ();

		if(level%2==0)
		{
			Magic++;
			SetFireBallDamage ();
			totalHealth += 5;
		}
		currentMagic = totalMagic;
		currentHealth = totalHealth;
		SetHealthBar ();
		SetMagicBar ();
		levelText.text = "LV." + level.ToString ("00");
	}
	/**
    * Pre: get hit to cost damage
    * Post: reduce the current health when player is hitted.
    * return: NA
    **/
	public void GetHit(float damage)
    {
		if (currentHealth <= 0) {
			currentHealth = 0;
			alive = false;
			print ("player out of health");
		} 
		else 
		{
			print ("player get hit");
            print(currentHealth);
			currentHealth -= damage;
			healthBar.Find("Fill_bar").GetComponent<Image>().fillAmount = currentHealth / totalHealth;
			//HP_Text.text = "HP: " + currentHealth.ToString () + "/" + totalHealth.ToString();
			HP_Text.text = "Event staff wage: " + currentHealth.ToString () + "/" + totalHealth.ToString();
		}
    }
	
    public void test_GetHit()
    {
        GetHit(10);
        if (currentHealth < totalHealth)
        {
            get_hit_test_text = UIController.instance.transform.Find("Tests/Result (2)").GetComponent<Text>();
            get_hit_test_text.text = "Passed";
        }
        else
        {
            get_hit_test_text = UIController.instance.transform.Find("Tests/Result (2)").GetComponent<Text>();
            get_hit_test_text.text = "Failed";
        }

        GetHit(10000);

        if (currentHealth <= 0)
        {
            get_hit_test_text = UIController.instance.transform.Find("Tests/Result (3)").GetComponent<Text>();
            get_hit_test_text.text = "Passed";
        }
        else
        {
            get_hit_test_text = UIController.instance.transform.Find("Tests/Result (3)").GetComponent<Text>();
            get_hit_test_text.text = "Failed";
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
	/**
    * Pre: Deal the damage
    * Post: cost enemy's health when get hit by player.
    * return: NA
    **/
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
	/**
    * Pre: Set Attack damage
    * Post: A way to set attack damage
    * return: NA
    **/
	void SetAttackDamage(){
		attackDamage = GameLogic.CalculatePlayerBaseAttackDamage(this) + bonusDamage;
	}
	/**
    * Pre: Set Fireball damage
    * Post: A way to set Fireball damage
    * return: NA
    **/
	void SetFireBallDamage(){
		FireBallDamage = GameLogic.CalculatePlayerFireBallDamage (this) + 10;
		FireBallDestroy.firedamage = FireBallDamage;
	}
	/**
    * Pre: Set Health bar
    * Post: Set the health bar image when the health keep reducing
    * return: NA
    **/
	void SetHealthBar()
	{
		healthBar.Find("Fill_bar").GetComponent<Image>().fillAmount = currentHealth / totalHealth;
		//HP_Text.text = "HP: " + currentHealth.ToString () + "/" + totalHealth.ToString();
		HP_Text.text = "Event staff wage: " + currentHealth.ToString () + "/" + totalHealth.ToString();
	}
	/**
    * Pre: Set Magic bar
    * Post: Set the health bar image when the magic keep reducing
    * return: NA
    **/
	void SetMagicBar()
	{
		abilityBar.Find("Fill_bar").GetComponent<Image>().fillAmount = currentMagic / totalMagic;
		//HP_Text.text = "HP: " + currentHealth.ToString () + "/" + totalHealth.ToString();
		MP_Text.text = "Energy: " + currentMagic.ToString () + "/" + totalMagic.ToString();
		isMPEmpty = false;
		AbilityControl1.filledImage.fillAmount = 0;
	}
	/**
    * Pre: Fireball using condition
    * Post: when the fireball is using then cost magic bar.
    * return: NA
    **/
	public void UseFireBall()
	{
		if (currentMagic <= 0) {
			currentMagic = 0;
			print ("player out of Energy");
			isMPEmpty = true;
		} 
		else 
		{
			print ("Cost energy");
			currentMagic -= AbilityControl1.MagicCost;
			abilityBar.Find("Fill_bar").GetComponent<Image>().fillAmount = currentMagic / totalMagic;
			//HP_Text.text = "HP: " + currentHealth.ToString () + "/" + totalHealth.ToString();
			MP_Text.text = "Energy: " + currentMagic.ToString () + "/" + totalMagic.ToString();
		}
	}
}
