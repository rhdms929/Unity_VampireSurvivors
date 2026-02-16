using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGem : MonoBehaviour
{
	public int expValue = 1;	// 경험치 보석이 제공하는 경험치 양
	public float moveSpeed = 5f;    // 보석이 플레이어를 향해 이동하는 속도
	private Transform target;  // 플레이어의 위치를 추적하기 위한 변수
	private bool isFollowing = false;  // 보석이 이동 중인지 여부를 나타내는 변수

	void OnEnable()
	{
		isFollowing = false;  // 보석이 활성화될 때 이동 상태 초기화
		target = null;  // 플레이어 위치 초기화
	}

	void Update()
	{
		if (!isFollowing || target == null) 
			return;
		transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
		moveSpeed += 0.2f;
	}

	public void StartFollow(Transform player)
	{
		target = player;
		isFollowing = true;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			GameManager.instance.AddExp();
			gameObject.SetActive(false);  // 보석을 비활성화하여 수집된 것으로 처리
		}
	}
}
