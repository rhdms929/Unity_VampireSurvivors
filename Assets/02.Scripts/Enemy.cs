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
        Vector2 nextVec = dirVec.normalized*speed * Time.fixedDeltaTime;
		rb.MovePosition(rb.position + nextVec);
        rb.velocity = Vector2.zero;
	}

	private void LateUpdate()
	{
		spriter.flipX = target.position.x < rb.position.x;
	}
}
