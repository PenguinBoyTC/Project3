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

	// Use this for initialization

	void Start () {
		AbilityActive = false;
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
		AbilityActive = true;
		isStartTimer = true;
	}
	public void TipButton()
	{
		if (Input.GetKey (KeyCode.Q)) 
		{
			AbilityActive = true;
			isStartTimer = true;
		}
	}
}
