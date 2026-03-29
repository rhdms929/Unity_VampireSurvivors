using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
	[Header("# Game Control")]
	public bool isLive;
	public float gameTime;
	public float maxGameTime = 2 * 10f; //20초

	[Header("# Player Stats")]
	public int playerId;
	public float health;
	public float maxHealth = 100;
	public int level;
	public int kill;
	public int exp;
	public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

	[Header("# Game Object")]
	public PoolManager pool;
	public Player player;
	public LevelUp LevelUpUI;
	public Result gameOverUI;
	public GameObject enemyCleaner;

	[Header("# Character Data")]
	public ItemData[] characterDatas; // 인스펙터에서 데이터 파일을 넣을 칸

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

	public void GameStart(int id)
	{
		playerId = id;
		health = maxHealth;

		player.gameObject.SetActive(true);

		//	튜버만 5번
		if (playerId == 2) 
		{
			LevelUpUI.Select(5); 
		}
		else
		{
			LevelUpUI.Select(playerId); 
		}

		isLive = true;
		ReStart();

		AudioManager.instance.PlayBgm(true);
		AudioManager.instance.PlaySfx(AudioManager.SFX.Select);
	}

	public void GameOver()
	{
		StartCoroutine(GameOverRoutine());
	}

	IEnumerator GameOverRoutine()
	{
		isLive = false;

		yield return new WaitForSeconds(0.5f);

		gameOverUI.gameObject.SetActive(true);
		gameOverUI.Lose();
		Stop();

		AudioManager.instance.PlayBgm(false);
		AudioManager.instance.PlaySfx(AudioManager.SFX.Lose);
	}

	public void GameVictory()
	{
		StartCoroutine(GameVictoryRoutine());
	}

	IEnumerator GameVictoryRoutine()
	{
		isLive = false;
		enemyCleaner.SetActive(false);

		yield return new WaitForSeconds(0.5f);

		gameOverUI.gameObject.SetActive(true);
		gameOverUI.Win();
		Stop();

		AudioManager.instance.PlayBgm(false);
		AudioManager.instance.PlaySfx(AudioManager.SFX.Win);
	}

	public void GameRestart()
	{
		SceneManager.LoadScene(0);
	}

	public void GameQuit()
	{
		Application.Quit();
	}

	void Update()
	{
		if(!isLive)
			return;
		gameTime += Time.deltaTime;

		if (gameTime > maxGameTime)
		{
			gameTime = maxGameTime;
			GameVictory();
		}
	}

	public void AddExp()
	{
		if(!isLive)
			return;
		exp++;

		if (exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
		{
			level++;
			exp = 0;
			LevelUpUI.Show();
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
