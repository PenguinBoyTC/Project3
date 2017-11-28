using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public float experience{ get; private set;}
	private int level;
	private Texture levelText;
	private Transform experienceBar;
    void Start () {
		level = 1;
		experienceBar = UIController.instance.transform.Find ("Background/Experience");
		levelText = UIController.instance.transform.Find ("Background/Level_Text").GetComponent<Text> ();
		SetExperience (0);
	}

   
    void Update () {
		
	}
	public void SetExperience(float exp){
		experience += exp;
		float experienceNeeded = GameLogic.ExperienceForNextLevel (level);
		float previousExperience = GameLogic.ExperienceForNextLevel (level - 1);
		if (experience >= experienceNeeded) {
			LevelUp ();
			experienceNeeded= GameLogic.ExperienceForNextLevel (level);
			previousExperience = GameLogic.ExperienceForNextLevel (level - 1);
		}
		experienceBar.Find ("Fill").GetComponent<Image> ().fillAmount = (experience - previousExperience) / (experienceNeeded - previousExperience);
	}
	void LevelUp(){
		level++;
		levelText.text = "LV." + level.ToString ("00");
	}
}
