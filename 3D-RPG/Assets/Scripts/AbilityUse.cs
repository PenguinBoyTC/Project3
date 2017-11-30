using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class AbilityUse : MonoBehaviour {

	public GameObject fireballPrefab;
	private FireBallAbility fba;
	private Stopwatch abilityCooldownTimer;
	private Button button;
	private Image fillImage;

	public void OnAbilityUse(GameObject btn)
	{
		//if ability is not on cool down use it
		fillImage = btn.transform.GetChild(0).gameObject.GetComponent<Image>();
		button = btn.GetComponent<Button> ();
		button.interactable = false;
		fillImage.fillAmount = 1;
		abilityCooldownTimer = new Stopwatch ();
		abilityCooldownTimer.Start ();

		GameObject go = Instantiate<GameObject> (fireballPrefab);
		go.transform.position = this.transform.position;
		fba = new FireBallAbility ();
		//fba.AbilityPrefab = go;
		//fba.UseAbiltiy (this.gameObject);

		StartCoroutine (SpinImage ());
	}
	private IEnumerator SpinImage()
	{
		//while (abilityCooldownTimer.IsRunning && abilityCooldownTimer.Elapsed.TotalSeconds < fba.AbilityCooldown) 
		//{
			//fillImage.fillAmount = ((float)abilityCooldownTimer.Elapsed.TotalSeconds / fba.AbilityCooldown);
			//yield return null;
		//}
		fillImage.fillAmount = 0;
		button.interactable = true;
		abilityCooldownTimer.Stop ();
		abilityCooldownTimer.Reset ();
		yield return null;
	}
}
