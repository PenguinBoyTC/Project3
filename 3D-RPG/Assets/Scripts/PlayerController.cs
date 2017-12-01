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

    public Text get_hit_test_text;


	private List<Transform> enemiesInRange = new List<Transform> ();
    private bool alive = true;
	public static bool isMPEmpty;

    void Start () {
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
            test_GetHit();
        }
	}

   
    void Update () {
		if (AbilityControl1.isMagicCost) 
		{
			UseFireBall ();
			AbilityControl1.isMagicCost = false;
		}
		
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
            get_hit_test_text.text = "Passed";
        }

        GetHit(1000);

        if (!alive)
        {
            get_hit_test_text = UIController.instance.transform.Find("Tests/Result (3)").GetComponent<Text>();
            get_hit_test_text.text = "Passed";
        }
        else
        {
            get_hit_test_text = UIController.instance.transform.Find("Tests/Result (3)").GetComponent<Text>();
            get_hit_test_text.text = "Passed";
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

	void SetFireBallDamage(){
		FireBallDamage = GameLogic.CalculatePlayerFireBallDamage (this) + 10;
		FireBallDestroy.firedamage = FireBallDamage;
	}
	void SetHealthBar()
	{
		healthBar.Find("Fill_bar").GetComponent<Image>().fillAmount = currentHealth / totalHealth;
		//HP_Text.text = "HP: " + currentHealth.ToString () + "/" + totalHealth.ToString();
		HP_Text.text = "Event staff wage: " + currentHealth.ToString () + "/" + totalHealth.ToString();
	}
	void SetMagicBar()
	{
		abilityBar.Find("Fill_bar").GetComponent<Image>().fillAmount = currentMagic / totalMagic;
		//HP_Text.text = "HP: " + currentHealth.ToString () + "/" + totalHealth.ToString();
		MP_Text.text = "Energy: " + currentMagic.ToString () + "/" + totalMagic.ToString();
	}
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
