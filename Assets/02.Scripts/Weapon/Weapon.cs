using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public const int Orbit = -100;      // 근접 무기 관통 플래그
	public const int Boomerang = -1;    // 부메랑 관통 플래그
	public const float BulletSpeed = 15f; // 발사 속도

	public float damage;
	public int penetrate;

	Rigidbody2D rb;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	public void Init(float damage, int penetrate, Vector3 dir)
	{
		this.damage = damage;
		this.penetrate = penetrate;

		if (penetrate >= 0)
		{
			rb.velocity = dir * BulletSpeed; // 발사 속도

		}
	}

	// 충돌 처리	
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Enemy") || penetrate == -Orbit)
			return;
		penetrate--;

		if(penetrate < 0)
		{
			rb.velocity = Vector2.zero;
			gameObject.SetActive(false);
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (!collision.CompareTag("Area") || penetrate == Orbit)
			return;

		gameObject.SetActive(false);
	}
}
