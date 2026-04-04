using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveManager : MonoBehaviour
{
	public GameObject[] lockCharacter;
	public GameObject[] unlockCharacter;
	public GameObject uiNotice;
	enum Achieve
	{
		unlockCharacter1,
		unlockCharacter2
	}
	Achieve[] achieves;
	WaitForSecondsRealtime wait;

	void Awake()
	{
		achieves = (Achieve[])Enum.GetValues(typeof(Achieve));
		wait = new WaitForSecondsRealtime(5);
		if (!PlayerPrefs.HasKey("MyData"))
		{
			Init();
		}
	}

	void Init()
	{
		PlayerPrefs.SetInt("MyData", 1);

		foreach (Achieve achieve in achieves)
		{
			PlayerPrefs.SetInt(achieve.ToString(), 0);
		}
	}

	void Start()
	{
		UnlockCharacter();
	}

	void UnlockCharacter()
	{
		for(int i=0; i<lockCharacter.Length; i++)
		{
			string achieveName = achieves[i].ToString();
			bool isUnlock = PlayerPrefs.GetInt(achieveName) == 1;
			lockCharacter[i].SetActive(!isUnlock);
			unlockCharacter[i].SetActive(isUnlock);
		}
	}

	public void CheckAllAchieves()
	{
		foreach (Achieve achieve in achieves)
			CheckAchieve(achieve);
	}

	void CheckAchieve(Achieve achieve)
	{
		bool isAchieve = false;

		switch (achieve)
		{
			case Achieve.unlockCharacter1:
				// Tuber: 적 10마리 처치
				isAchieve = GameManager.instance.kill >= 10;
				break;
			case Achieve.unlockCharacter2:
				// Corny: 레벨 5 달성 시 해금
				isAchieve = GameManager.instance.level >= 5;
				break;
		}

		if (isAchieve && PlayerPrefs.GetInt(achieve.ToString()) == 0)
		{
			PlayerPrefs.SetInt(achieve.ToString(), 1);

			for(int i=0; i<uiNotice.transform.childCount; i++)
			{
				bool isActive = i == (int)achieve;
				uiNotice.transform.GetChild(i).gameObject.SetActive(isActive);
			}

			StartCoroutine(NoticeRoutine());
		}
	}

	IEnumerator NoticeRoutine()
	{
		uiNotice.SetActive(true);
		AudioManager.instance.PlaySfx(AudioManager.SFX.LevelUp);

		yield return wait;

		uiNotice.SetActive(false);
	}
}
