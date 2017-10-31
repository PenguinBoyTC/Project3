using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public Texture2D frame;
	public Rect framePosition;
	
	public Texture2D healthBar;
	public Rect healthBarPosition;
	
	public float horizontalDistance;
	public float verticalDistance;
	public float width;
	public float height;
	public float healthPercentage;
	public Fighter player;
	public Mob target;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI()
	{
		drawFrame();
		drawBar();
	}
	void drawFrame()
	{
		framePosition.x = (Screen.width - framePosition.width)/2;
		framePosition.width = Screen.width*0.39f;
		framePosition.height = Screen.height*0.0625f;
		GUI.DrawTexture(framePosition, frame);
	}
	void drawBar()
	{
		healthBarPosition.x = framePosition.x + framePosition.width * horizontalDistance;
		healthBarPosition.y = framePosition.y + framePosition.height * verticalDistance;
		healthBarPosition.width = framePosition.width*width;
		healthBarPosition.height = framePosition.height*height;
		GUI.DrawTexture(healthBarPosition, healthBar);
	}
}
