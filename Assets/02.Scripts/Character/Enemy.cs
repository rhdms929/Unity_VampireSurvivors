using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[Header("# Enemy Stats")]
	public float speed;
	public float health;
	public float maxHealth;
	public RuntimeAnimatorController[] animator;
	public Rigidbody2D target;

	bool isLive;

	Rigidbody2D rb;
	Collider2D col;
	Animator anim;
	SpriteRenderer spriter;
	WaitForFixedUpdate wait;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
		anim = GetComponentInChildren<Animator>();
		spriter = GetComponent<SpriteRenderer>();
		wait = new WaitForFixedUpdate();
	}
	void FixedUpdate()
	{
		// 게임이 멈췄거나 적이 죽었다면 중단
		if (!GameManager.instance.isLive || !isLive)
			return;

		if (anim.GetCurrentAnimatorStateInfo(0).IsName("DAMAGED"))
			return;

		Vector2 dirVec = target.position - rb.position;
		Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

		rb.MovePosition(rb.position + nextVec);
		rb.velocity = Vector2.zero;
	}

	void LateUpdate()
	{
		if (!GameManager.instance.isLive || target == null) 
			return;

		// 플레이어가 나보다 왼쪽에 있는지 판정
		bool isLeft = target.position.x < rb.position.x;

		spriter.flipX = isLeft;
	}

	void OnEnable()
	{
		target = GameManager.instance.player.GetComponent<Rigidbody2D>();
		isLive = true;
		col.enabled = true;
		rb.simulated = true;
		spriter.sortingOrder = 2; 
		anim.SetBool("Dead", false);
		health = maxHealth;

		// 다시 소환될 때 투명도 초기화
		SpriteRenderer[] childrenSprites = GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer s in childrenSprites)
		{
			Color c = s.color;
			c.a = 1f;
			s.color = c;
		}
	}

	public void Init(SpawnData data)
	{
		anim.runtimeAnimatorController = animator[data.spriteType];
		speed = data.speed;
		maxHealth = data.health;
		health = data.health;
	}

	//	죽음 처리
	void OnTriggerEnter2D(Collider2D collision)
	{
		if(!collision.CompareTag("Weapon") || !isLive)
			return;

		health -= collision.GetComponent<Weapon>().damage;

		StartCoroutine(KnockBack());

		if (health > 0)
		{
			anim.SetTrigger("Damaged");
		}
		else
		{
			// Die Animation
			isLive = false;
			col.enabled = false;    // 시체와는 더 이상 부딪히지 않게
			rb.simulated = false;
			spriter.sortingOrder = 1;  // 살아있는 몬스터들을 가리지않게 하기위함
			anim.SetBool("Dead", true);

			StartCoroutine(FadeOut());
			GameManager.instance.kill++;
			GameManager.instance.AddExp();
		}
	}

	IEnumerator FadeOut()
	{
		yield return new WaitForSeconds(1.0f); // 죽는 애니메이션 볼 시간 확보

		SpriteRenderer[] childrenSprites = GetComponentsInChildren<SpriteRenderer>();
		float alpha = 1f;

		while (alpha > 0)
		{
			alpha -= Time.deltaTime;
			foreach (SpriteRenderer s in childrenSprites)
			{
				Color c = s.color;
				c.a = alpha;
				s.color = c;
			}
			yield return null;
		}

		Dead(); // 완전히 투명해지면 비활성화
	}

	IEnumerator KnockBack()
	{
		yield return wait;  // 물리 프레임 딜레이
		Vector3 playerPos = GameManager.instance.player.transform.position;
		Vector3 dirVec = transform.position - playerPos;

		rb.AddForce(dirVec.normalized * 0.5f, ForceMode2D.Impulse);
	}

	void Dead()
	{
		// 50% 확률로만 보석 생성
		if (Random.Range(0f, 1f) > 0.5f)
		{
			GameObject gem = GameManager.instance.pool.Get(4);
			gem.transform.position = transform.position;
		}

		gameObject.SetActive(false);
	}
}
