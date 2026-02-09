using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUpgrade : MonoBehaviour
{
	public ItemData data;
	public int level;
	public Weapon weapon;
	public Gear gear;

	Image icon;
	Text textLevel;

	void Awake()
	{
		icon = GetComponentsInChildren<Image>()[1];
		icon.sprite = data.itemIcon;

		Text[] texts = GetComponentsInChildren<Text>();
		textLevel = texts[0];
	}

	void LateUpdate()
	{
		textLevel.text = "Lv. " + (level + 1);  //	level 1부터 시작
	}

	public void OnClick()
	{
		Player player = GameManager.instance.player;

		switch (data.itemType)
		{
			case ItemData.ItemType.Melee:
			case ItemData.ItemType.Ranged:
				if (level == 0)
				{
					// 1. 새로운 게임 오브젝트 생성 (실시간 생성)
					GameObject newWeaponObj = new GameObject("Weapon_" + data.itemID);

					// 2. 플레이어의 자식으로 설정
					newWeaponObj.transform.parent = player.transform;
					newWeaponObj.transform.localPosition = Vector3.zero;

					// 3. WeaponManager 컴포넌트 추가 및 설정
					WeaponManager targetWeapon = newWeaponObj.AddComponent<WeaponManager>();
					targetWeapon.SetStrategy(data);
				}
				else
				{
					// 레벨업 시에는 플레이어 자식 중 해당 무기를 찾아서 처리
					WeaponManager[] managers = player.GetComponentsInChildren<WeaponManager>();
					foreach (WeaponManager wm in managers)
					{
						if (wm.id == data.itemID)
						{
							float nextDamage = data.baseDamage * data.growthDamage[level - 1];
							int nextCount = data.growthCount[level - 1];
							wm.LevelUp(nextDamage, nextCount);
							break;
						}
					}
				}
				level++;
				break;
			case ItemData.ItemType.Glove:
			case ItemData.ItemType.Shoes:
				if(level == 0)
				{
					// 1. 새로운 게임 오브젝트 생성 (실시간 생성)
					GameObject newGearObj = new GameObject("Gear_" + data.itemID);
					// 2. 플레이어의 자식으로 설정
					newGearObj.transform.parent = player.transform;
					newGearObj.transform.localPosition = Vector3.zero;
					// 3. Gear 컴포넌트 추가 및 설정
					Gear targetGear = newGearObj.AddComponent<Gear>();
					targetGear.init(data);
				}
				else
				{
					// 레벨업 시에는 플레이어 자식 중 해당 장비를 찾아서 처리
					Gear[] gears = player.GetComponentsInChildren<Gear>();
					foreach (Gear g in gears)
					{
						if (g.type == data.itemType)
						{
							float nextRate = data.growthDamage[level - 1];
							g.LevelUp(nextRate);
							break;
						}
					}
				}
				level++;
				break;
			case ItemData.ItemType.Health:
				GameManager.instance.health = GameManager.instance.maxHealth;
				break;
		}
		player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);

		if(level >= data.growthDamage.Length)
		{
			GetComponent<Button>().interactable = false;
		}
	}
}
