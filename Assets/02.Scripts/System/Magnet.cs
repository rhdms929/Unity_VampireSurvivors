using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D collision)
	{
		// 닿은 물체의 태그가 Gem인지 확인
		if (collision.CompareTag("Gem"))
		{
			ExpGem gem = collision.GetComponent<ExpGem>();
			if (gem != null)
				gem.StartFollow(transform.parent);
		}
	}
}
