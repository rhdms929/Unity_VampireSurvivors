using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public SpawnData[] spawnDatas;

	int level;
    float timer;

    void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
	}

	void Update()
    {
		if (!GameManager.instance.isLive)
			return;

		timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnDatas.Length - 1);

		if (timer > spawnDatas[level].spawnTime)
        {
            timer = 0f;
            Spawn();
		}
	}

	void Spawn()
	{
		// 0번(Enemy1) 또는 1번(Enemy2) 중 랜덤으로 결정
		int ranType = Random.Range(0, 2);

		// PoolManager에서 랜덤하게 꺼내오기
		GameObject enemy = GameManager.instance.pool.Get(ranType);

		enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
		enemy.GetComponent<Enemy>().Init(spawnDatas[level]);
	}
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}
