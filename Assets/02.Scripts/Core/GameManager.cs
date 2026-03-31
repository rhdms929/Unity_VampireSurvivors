using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[Header("# Game Control")]
	public bool isLive;
	public float gameTime;
	public float maxGameTime = 2 * 10f;

	[Header("# Player Stats")]
	public int playerId; // 실제 게임 중인 캐릭터 ID
	public int selectId; // 캐릭터 선택창에서 임시로 저장할 ID
	public float health;
	public float maxHealth = 100;
	public int level;
	public int kill;
	public int exp;
	public int[] nextExp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };
	public int MaxLevel => nextExp.Length;

	[Header("# Game Object")]
	public PoolManager pool;
	public Player player;
	public LevelUp LevelUpUI;
	public Result gameOverUI;
	public GameObject enemyCleaner;
	public GameObject startUI; // 시작 화면 UI 그룹
	public AchieveManager achieveManager;

	[Header("# Character Data")]
	public ItemData[] characterDatas;
	public ItemData SelectedCharacterData => characterDatas[playerId];

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else Destroy(gameObject);
	}

	public void SelectCharacter(int id)
	{
		selectId = id;
		Debug.Log(id + "번 캐릭터 선택됨. 스타트 버튼을 누르세요");
		AudioManager.instance.PlaySfx(AudioManager.SFX.Select);
	}

	public void RealStart()
	{
		GameStart(selectId);
		if (startUI != null) startUI.SetActive(false);
	}

	public void GameStart(int id)
	{
		playerId = id;
		health = maxHealth;
		player.gameObject.SetActive(true);

		// startWeaponIds 배열 기반으로 시작 무기 지급
		foreach (int weaponId in SelectedCharacterData.startWeaponIds)
		{
			LevelUpUI.Select(weaponId);
		}

		isLive = true;
		ReStart();
		AudioManager.instance.PlayBgm(true);
	}

	public void GameOver()
	{
		StartCoroutine(GameEndRoutine(false));
	}

	public void GameVictory()
	{
		StartCoroutine(GameEndRoutine(true));
	}

	IEnumerator GameEndRoutine(bool isWin)
	{
		isLive = false;
		achieveManager.CheckAllAchieves();

		if (isWin)
			enemyCleaner.SetActive(false);

		yield return new WaitForSeconds(0.5f);

		gameOverUI.gameObject.SetActive(true);

		if (isWin) gameOverUI.Win();
		else gameOverUI.Lose();

		Stop();
		AudioManager.instance.PlayBgm(false);
		AudioManager.instance.PlaySfx(isWin ? AudioManager.SFX.Win : AudioManager.SFX.Lose);
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
		if (!isLive) return;

		gameTime += Time.deltaTime;

		if (gameTime > maxGameTime)
		{
			gameTime = maxGameTime;
			GameVictory();
		}
	}
	public void AddKill()
	{
		kill++;
		achieveManager.CheckAllAchieves(); // 킬 업적 체크
	}

	public void AddExp()
	{
		if (!isLive) return;

		exp++;

		int currentLevelExp = nextExp[Mathf.Min(level, MaxLevel - 1)];
		if (exp >= currentLevelExp)
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
