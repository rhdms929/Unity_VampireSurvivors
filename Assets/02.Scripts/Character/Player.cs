using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Vector2 inputVec; 
	public float moveSpeed = 5f;
	public Scanner scanner;
	public bool isDead;
	public RuntimeAnimatorController[] animCon;

	public float baseMoveSpeed;
	Rigidbody2D rb;
	SpriteRenderer spriter;
	Animator anim;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		spriter = GetComponent<SpriteRenderer>();
		scanner = GetComponent<Scanner>();
		baseMoveSpeed = moveSpeed;
	}

	void OnEnable()
	{
		isDead = false;
		moveSpeed = baseMoveSpeed * Character.Speed;
		anim.runtimeAnimatorController = animCon[GameManager.instance.playerId];
	}

	void Update()
	{
		if(!GameManager.instance.isLive)
			return;

		float moveX = Input.GetAxisRaw("Horizontal");
		float moveY = Input.GetAxisRaw("Vertical");
		inputVec = new Vector2(moveX, moveY).normalized;

		// 애니메이터
		if (anim != null)
		{
			anim.SetFloat("Speed", inputVec.magnitude);
		}
		if(moveX != 0)
		{
			spriter.flipX = moveX < 0;
		} 
	}

	void FixedUpdate()
	{
		if (!GameManager.instance.isLive)
			return;
		rb.MovePosition(rb.position + inputVec * moveSpeed * Time.fixedDeltaTime);
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		if (!GameManager.instance.isLive || isDead) 
			return;

		GameManager.instance.health -= Time.deltaTime * 10;

		if (GameManager.instance.health < 0)
		{
			Die(); 
		}
	}

	void Die()
	{
		isDead = true;

		for (int i = 2; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}

		anim.SetTrigger("Dead");
		GameManager.instance.GameOver();
	}
}
