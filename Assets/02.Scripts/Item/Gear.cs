using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
	public ItemData.ItemType type;
	public float rate;

	public void Init(ItemData data)
	{
		name = "Gear_" + data.itemID;
		transform.parent = GameManager.instance.player.transform;
		transform.localPosition= Vector3.zero;

		type = data.itemType;
		rate = data.growthDamage[0];
		ApplyGear();
	}

	public void LevelUp(float nextRate)
	{
		this.rate = nextRate;
		ApplyGear();
	}

	void ApplyGear()    // 타입에 따라 적절하게 로직 적용하는 함수
	{
		switch (type) {
			case ItemData.ItemType.Glove:
				RateUp();
				break;
			case ItemData.ItemType.Shoes:
				SpeedUp();
				break;
		}
	}


	void RateUp()
	{
		WeaponManager[] weapons = transform.parent.GetComponentsInChildren<WeaponManager>();

		foreach (WeaponManager weapon in weapons)
		{
			if (weapon.id == 0) // 근접 무기(회전)
			{
				// 속도가 절대 0 이하로 내려가지 않도록 Mathf.Max를 사용
				float calculatedSpeed = 150f + (150f * rate);
				weapon.speed = Mathf.Max(calculatedSpeed, 50f);
			}
			else // 원거리 무기
			{
				// 발사 간격은 너무 작아지면 무한 발사가 되므로 최소값 제한
				float calculatedInterval = 0.5f * (1f - rate);
				weapon.speed = Mathf.Max(calculatedInterval, 0.1f);
			}
		}
	}

	void SpeedUp()  //	신발기능 이동속도 올리는 함수
	{
		float defaultSpeed = 5f;
		GameManager.instance.player.moveSpeed = defaultSpeed * (1f + rate);
	}

}
