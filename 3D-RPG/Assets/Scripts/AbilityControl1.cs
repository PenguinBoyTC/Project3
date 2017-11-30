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
	// Use this for initialization

	void Start () {
		AbilityActive = false;
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
		yield return new WaitForSeconds (FireTime);
		AbilityActive = false;
	}
	public void TipButton()
	{
		if (Input.GetKey (KeyCode.Q)) 
		{
			AbilityActive = true;
			isStartTimer = true;
			yield return new WaitForSeconds (FireTime);
			AbilityActive = false;
		}
	}
}
