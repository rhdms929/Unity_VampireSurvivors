using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed;
	public float health;
	public float maxHealth;
	public RuntimeAnimatorController[] animator;
	public Rigidbody2D target;

	bool isLive;

	Rigidbody2D rb;
	Animator anim;
	SpriteRenderer spriter;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
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
		isLive = true;
		health = maxHealth;
	}

	public void Init(SpawnData data)
	{
		anim.runtimeAnimatorController = animator[data.spriteType];
		speed = data.speed;
		maxHealth = data.health;
		health = data.health;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(!collision.CompareTag("Weapon"))
			return;

		health -= collision.GetComponent<Weapon>().damage;

		if(health > 0)
		// Live Hit Animation
		{

		}
		else
		{
			// Die Animation
			Dead();
		}
	}

	void Dead()
	{
		gameObject.SetActive(false);
	}
}
