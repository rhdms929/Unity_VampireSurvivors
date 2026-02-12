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


	void RateUp()   // 장갑기능 연사력 올리는 함수
	{
		WeaponManager[] weapons = transform.parent.GetComponentsInChildren<WeaponManager>();

		foreach (WeaponManager weapon in weapons)
		{
			float baseSpd = weapon.data.baseSpeed;
			weapon.speed = baseSpd * (1f - rate);   // 기본 간격 * (1 - 감소율)
		}
	}

	void SpeedUp()  //	신발기능 이동속도 올리는 함수
	{
		float defaultSpeed = 5f;
		GameManager.instance.player.moveSpeed = defaultSpeed * (1f + rate);
	}

}
