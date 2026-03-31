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
		rate = nextRate;
		ApplyGear();
	}

	public void ApplyGear()    // 타입에 따라 적절하게 로직 적용하는 함수
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
			// 무기 아이디나 타입을 기준으로 분기 처리
			switch (weapon.type)
			{
				case WeaponManager.WeaponType.Orbit: // 근접 무기
					float baseOrbitSpeed = 150f * Character.WeaponSpeed;
					weapon.speed = baseOrbitSpeed * (1f + rate);
					break;

				case WeaponManager.WeaponType.Fire: // 원거리 무기
					float baseFireRate = 0.5f * Character.WeaponRate;
					weapon.speed = Mathf.Max(baseFireRate * (1f - rate), 0.1f);
					break;

				case WeaponManager.WeaponType.Boomerang: // 부메랑
					float baseBoomerangRate = 0.8f * Character.WeaponRate;
					weapon.speed = Mathf.Max(baseBoomerangRate * (1f - rate), 0.1f);
					break;
			}
		}
	}

	void SpeedUp()  //	신발기능 이동속도 올리는 함수
	{
		Player player = GameManager.instance.player;
		player.moveSpeed = player.baseMoveSpeed * Character.Speed * (1f + rate);
	}

}
