using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed;
	public Rigidbody2D target;

	bool isLive;

	Rigidbody2D rb;
	SpriteRenderer spriter;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		spriter = GetComponent<SpriteRenderer>();
	}
	void FixedUpdate()
	{
		Vector2 dirVec = target.position - rb.position;
		Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
		rb.MovePosition(rb.position + nextVec);
		rb.velocity = Vector2.zero;
	}

	void LateUpdate()
	{
		if (target == null)
			return;
		// 플레이어가 몬스터보다 왼쪽에 있는지 판정
		bool isLeft = target.position.x < rb.position.x;

		transform.localScale = new Vector3(isLeft ? 1f : -1f, 1f, 1f);
	}

	void OnEnable()
	{
		target = GameManager.instance.player.GetComponent<Rigidbody2D>();
	}
}
