using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public enum InfoType 
	{
		Exp, 
		Level, 
		Kill, 
		Time, 
		Health 
	}
	public InfoType infoType;

	Text myText;
	Slider mySlider;

	void Awake()
	{
		myText = GetComponent<Text>();
		mySlider = GetComponent<Slider>();
	}

	void LateUpdate()
	{
		// GameManager Ä³½Ì
		GameManager gm = GameManager.instance;

		switch (infoType)
		{
			case InfoType.Exp:
				float curExp = gm.exp;
				float maxExp = gm.nextExp[Mathf.Min(gm.level, gm.nextExp.Length - 1)];
				mySlider.value = curExp / maxExp;
				break;
			case InfoType.Level:
				myText.text = string.Format("Lv.{0:D2}", gm.level);
				break;
			case InfoType.Kill:
				myText.text = string.Format("{0:F0}", gm.kill);
				break;
			case InfoType.Time:
				float remainTime = gm.maxGameTime - gm.gameTime;
				int minutes = Mathf.FloorToInt(remainTime / 60f);
				int seconds = Mathf.FloorToInt(remainTime % 60);
				myText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
				break;
			case InfoType.Health:
				mySlider.value = gm.health / gm.maxHealth;
				break;
		}
	}
}