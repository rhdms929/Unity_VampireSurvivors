using UnityEngine;

public class Reposition : MonoBehaviour
{
	void OnTriggerExit2D(Collider2D collision) 
	{
		//	¹«ÇÑ ¸Ê ÀÌµ¿
		if (!collision.CompareTag("Area"))
			return;

		Vector3 playerPos = GameManager.instance.player.transform.position;
		Vector3 myPos = transform.position;

		float diffX = playerPos.x - myPos.x;
		float diffY = playerPos.y - myPos.y;

		Vector3 playerDir = GameManager.instance.player.inputVec; 

		float absDiffX = Mathf.Abs(diffX);
		float absDiffY = Mathf.Abs(diffY);

		switch (transform.tag)
		{
			case "Ground":
				if (absDiffX > absDiffY)
				{
					transform.Translate(Vector3.right * Mathf.Sign(diffX) * 40);
				}
				else if (absDiffX < absDiffY)
				{
					transform.Translate(Vector3.up * Mathf.Sign(diffY) * 40);
				}
				break;

			case "Enemy":
				break;
		}
	}
}