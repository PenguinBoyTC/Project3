using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityControl1 : MonoBehaviour {

	public float coldTime;
	private float timer = 0;
	private Image filledImage;
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
		FireBallDestroy.firelong = FireLong;
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
			filledImage.fillAmount = (coldTime - timer) / coldTime;
		
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
