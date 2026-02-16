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
			// 보석 스크립트를 가져와서 StartFollow 실행!
			// 보석에게 플레이어(내 부모)의 위치를 넘겨줍니다.
			collision.GetComponent<ExpGem>().StartFollow(transform.parent);
		}
	}
}
