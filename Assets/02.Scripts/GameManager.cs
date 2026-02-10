using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
	[Header("# Game Control")]
	public bool isLive;
	public float gameTime;
	public float maxGameTime = 2 * 10f; //20초
	[Header("# Player Stats")]
	public int health;
	public int maxHealth = 100;
	public int level;
	public int kill;
	public int exp;
	public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 550 };
	[Header("# Game Object")]
	public PoolManager pool;
	public Player player;
	public LevelUp uiLevelUp;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		health = maxHealth;

		//	임시 스크립트 (첫번째 캐릭터 선택)
		uiLevelUp.Select(0);
	}

	void Update()
	{
		if(!isLive)
			return;
		gameTime += Time.deltaTime;

		if (gameTime > maxGameTime)
		{
			gameTime = maxGameTime;
		}
	}

	public void AddExp()
	{
		exp++;

		if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
		{
			level++;
			exp = 0;
			uiLevelUp.Show();
		}
	}

	public void Stop()
	{
		isLive = false;
		Time.timeScale = 0f;
	}

	public void ReStart()
	{
		isLive = true;
		Time.timeScale = 1f;
	}
}
