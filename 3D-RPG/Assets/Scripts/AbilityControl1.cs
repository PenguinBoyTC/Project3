using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityControl1 : MonoBehaviour {

	public float coldTime;
	private float timer = 0;
	//private Image filledImage;
	public static Image filledImage;
	private bool isStartTimer;
	public static bool AbilityActive;
	public float FireTime;
	public float FireLong;
	public float FireDamage;
	public float FireDamageRange;
	public float FireSpeed;
	public static float MagicCost;
	public static bool isMagicCost;
	// Use this for initialization

	void Start () {
		MagicCost = 5;
		AbilityActive = false;
		isMagicCost = false;
		FireBallDestroy.firelong = FireLong;//control the distance of the fireball
		FireBallDestroy.firedamage = FireDamage;
		FireBallDestroy.fireRange = FireDamageRange;
		FireBallAbility.fireInterval = FireSpeed;
		filledImage = transform.Find("Q_CD").GetComponent<Image>();
	}

	// Update is called once per frame
	void Update () {

		TipButton ();
		if (isStartTimer)
		{
			//AbilityActive = true;
			timer += Time.deltaTime;
			if (timer >= 0 && timer < 0.03) 
			{
				isMagicCost = true;
			}
			filledImage.fillAmount = (coldTime - timer) / coldTime;//set the colddown cover
		
		}
		if (timer > FireTime) 
		{
			AbilityActive = false;
		} 
		if (timer >= coldTime)
		{
			filledImage.fillAmount = 0;
			timer = 0;
			isStartTimer = false;
		}
	}
    /**
    * Pre: click the fireball icon to control the ability
    * Post: to control if the fireball is available when click the icon.
    * return: NA
    **/
	public void OnClick()
	{
		if (!PlayerController.isMPEmpty) {
			AbilityActive = true;
			isStartTimer = true;
		} 
		else
		{
			AbilityActive = false;
			isStartTimer = false;
		}
	}
	/**
    * Pre: Tip the key board to control the ability
    * Post: to control if the fireball is available when tip the Q button.
    * return: NA
    **/
	public void TipButton()
	{
		if (!PlayerController.isMPEmpty) {
			if (Input.GetKey (KeyCode.Q)) {
				AbilityActive = true;
				isStartTimer = true;
			}
		} 
		else
		{
			AbilityActive = false;
			isStartTimer = false;
		}
	}
}
