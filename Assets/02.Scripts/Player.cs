using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Vector2 inputVec; 
	public float moveSpeed = 5f;
	public Scanner scanner;

	Rigidbody2D rb;
	Animator anim;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
		scanner = GetComponent<Scanner>();
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
			anim.SetBool("1_Move", inputVec.magnitude > 0);
		}
		//	방향
		if (moveX != 0)
		{
			anim.transform.localScale = new Vector3(moveX > 0 ? -1 : 1, 1, 1);
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
		if(!GameManager.instance.isLive)
			return;

		GameManager.instance.health -= Time.deltaTime * 10;

		if(GameManager.instance.health < 0)
			{
				for(int i=2; i < transform.childCount; i++)
				{
					transform.GetChild(i).gameObject.SetActive(false);
				}
			anim.SetTrigger("Dead");
		}
		
	}
}