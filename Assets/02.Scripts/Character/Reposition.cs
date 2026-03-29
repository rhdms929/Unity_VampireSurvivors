using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
	Collider2D col;
	void Awake() 
	{
		col = GetComponent<Collider2D>();
	}

	void OnTriggerExit2D(Collider2D collision) 
	{
		//	¹«ÇÑ ¸Ê ÀÌµ¿
		if (!collision.CompareTag("Area"))
			return;

		Vector3 playerPos = GameManager.instance.player.transform.position;
		Vector3 myPos = transform.position;


		switch (transform.tag)
		{
			case "Ground":
				float diffX = playerPos.x - myPos.x;
				float diffY = playerPos.y - myPos.y;
				float dirx = diffX < 0 ? -1 : 1;
				float diry = diffY < 0 ? -1 : 1;
				diffX = Mathf.Abs(diffX);
				diffY = Mathf.Abs(diffY);

				if (diffX > diffY)
				{
					transform.Translate(Vector3.right * dirx * 40);
				}
				else if (diffX < diffY)
				{
					transform.Translate(Vector3.up * diry * 40);
				}
				break;

			case "Enemy":
				if (col.enabled)
				{
					Vector3 dist = playerPos - myPos;
					Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
					transform.Translate(ran + dist * 2);
				}
				break;
		}
	}
}