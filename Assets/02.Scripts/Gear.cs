using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
	public ItemData.ItemType type;
	public float rate;

	public void init(ItemData data)
	{
		// Basic Set
		name = "Gear_" + data.itemID;
		transform.parent = GameManager.instance.player.transform;
		transform.localPosition= Vector3.zero;

		// Property Set
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


	void RateUp()   // 장갑기능 연사력 올리는 함수
	{
		WeaponManager[] weapons = transform.parent.GetComponentsInChildren<WeaponManager>();

		foreach (WeaponManager weapon in weapons)
		{
			switch (weapon.id)
			{
				case 0:
					weapon.speed = 150 + (150 * rate);
					break;
				default:
					weapon.speed = 0.5f * (1f - rate);
					break;
			}
		}
	}

	void SpeedUp()  //	신발기능 이동속도 올리는 함수
	{
		float speed = 5;
		GameManager.instance.player.moveSpeed = speed + (speed * rate);
	}

}
