using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
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
			rb.velocity = dir * 15f; // 惯荤 加档

		}
	}

	// 面倒 贸府	
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Enemy") || penetrate == -100)
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
		if (!collision.CompareTag("Area") || penetrate == -100)
			return;

		gameObject.SetActive(false);
	}
}
