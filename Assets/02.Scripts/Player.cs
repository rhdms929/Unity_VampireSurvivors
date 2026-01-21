using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Vector2 moveInput;
	public float moveSpeed;
    Rigidbody2D rb;

	void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
	}


    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
		moveInput = new Vector2(moveX, moveY).normalized;

		if(moveX != 0)
		{
			transform.localScale = new Vector3(-moveX, 1, 1);
		}
	}

	private void FixedUpdate()
	{
		rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
	}
}
