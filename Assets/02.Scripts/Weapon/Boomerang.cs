using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
	public float damage;

	[Header("# Boomerang Settings")]
	public float gravity = 25f;
	public float initialVerticalSpeed = 12f;
	public float relativeStartHeight = 0.5f;
	public float deactivateYThreshold = -5f;

	float speed;
	Vector3 dir;
	Transform player;
	Vector3 verticalVelocity;
	Vector3 relativePos;

	public void Init(float damage, float speed, Vector3 dir, Transform player)
	{
		this.damage = damage;
		this.speed = speed;
		this.dir = (dir + Vector3.up * 1.5f).normalized;
		this.player = player;

		verticalVelocity = Vector3.up * initialVerticalSpeed;
		relativePos = Vector3.up * relativeStartHeight;
	}

	void Update()
	{
		if (player == null) return;

		// 1. 도끼 자전
		transform.Rotate(Vector3.forward * -800f * Time.deltaTime);

		// 2. 포물선 궤적 계산 (상대 좌표)
		verticalVelocity += Vector3.down * gravity * Time.deltaTime;
		relativePos += (dir * speed + verticalVelocity) * Time.deltaTime;

		// 3. [실시간 추적 핵심] 플레이어의 현재 위치 + 계산된 상대 위치
		transform.position = player.position + relativePos;

		// 4. 화면 아래로 떨어지면 회수
		if (relativePos.y < deactivateYThreshold)
			gameObject.SetActive(false);
	}
}