using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
	[Header("# Game Control")]
	public float gameTime;
	public float maxGameTime = 2 * 10f; //20ÃÊ
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
	}

	void Update()
	{
		gameTime += Time.deltaTime;

		if (gameTime > maxGameTime)
		{
			gameTime = maxGameTime;
		}
	}

	public void AddExp()
	{
		exp++;

		if (exp == nextExp[level])
		{
			level++;
			exp = 0;
		}
	}
}
