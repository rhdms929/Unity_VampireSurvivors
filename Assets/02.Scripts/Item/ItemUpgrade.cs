using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUpgrade : MonoBehaviour
{
	public ItemData data;
	public int level;

	[Header("# UI References")]
	[SerializeField] private Image icon;
	[SerializeField] private Text textLevel;
	[SerializeField] private Text textName;
	[SerializeField] private Text textDesc;

	void Awake()
	{
		// 데이터가 연결되어 있다면 아이콘과 이름을 초기 설정
		if (data != null)
		{
			icon.sprite = data.itemIcon;
			textName.text = data.itemName;
		}
	}

	void OnEnable()
	{
		UpdateUI();
	}

	void UpdateUI()
	{
		textLevel.text = "Lv. " + (level + 1);

		bool isWeapon = (data.itemType == ItemData.ItemType.Melee ||
							data.itemType == ItemData.ItemType.Ranged ||
							data.itemType == ItemData.ItemType.Boomerang);

		if (isWeapon)
		{
			// 무기: 데미지와 개수 표시
			textDesc.text = string.Format(data.itemDesc, data.growthDamage[level] * 100, data.growthCount[level]);
		}
		else
		{
			// 장비: 상승률만 표시
			textDesc.text = string.Format(data.itemDesc, data.growthDamage[level] * 100);
		}
	}

	public void OnClick()
	{
		Player player = GameManager.instance.player;

		// 아이템 타입에 따른 분기 처리
		switch (data.itemType)
		{
			case ItemData.ItemType.Melee:
			case ItemData.ItemType.Ranged:
			case ItemData.ItemType.Boomerang:
				HandleWeaponUpgrade(player); // 무기 로직 실행
				break;
			case ItemData.ItemType.Glove:
			case ItemData.ItemType.Shoes:
				HandleGearUpgrade(player);   // 장비 로직 실행
				break;
			case ItemData.ItemType.Health:
				// 체력 회복 아이템: 즉시 최대 체력으로 회복
				GameManager.instance.health = GameManager.instance.maxHealth;
				break;
		}

		level++;
		UpdateUI(); // 레벨업 후 즉시 UI 갱신

		// 장비 효과를 모든 무기에 전파
		player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);

		// 최대 레벨에 도달하면 더 이상 선택할 수 없게 버튼 비활성화
		if (level >= data.growthDamage.Length)
		{
			GetComponent<Button>().interactable = false;
		}
	}

	void HandleWeaponUpgrade(Player player)
	{
		if (level == 0) // 첫 획득 시: 장비 오브젝트 생성 및 초기화
		{
			GameObject newWeaponObj = new GameObject("Weapon_" + data.itemID);
			newWeaponObj.transform.parent = player.transform;
			newWeaponObj.transform.localPosition = Vector3.zero;
			WeaponManager targetWeapon = newWeaponObj.AddComponent<WeaponManager>();
			targetWeapon.SetStrategy(data);
		}
		else // 레벨업 시: 기존 무기 매니저를 찾아 수치 갱신
		{
			WeaponManager[] managers = player.GetComponentsInChildren<WeaponManager>();
			foreach (WeaponManager wm in managers)
			{
				if (wm.id == data.itemID)
				{
					// 데이터 시트 기반으로 다음 레벨 능력치 계산
					float nextDamage = data.baseDamage * (1 + data.growthDamage[level - 1]);
					int nextCount = data.growthCount[level - 1];
					wm.LevelUp(nextDamage, nextCount);
					break;
				}
			}
		}
	}

	void HandleGearUpgrade(Player player)
	{
		if (level == 0) // 첫 획득 시: 장비 오브젝트 생성 및 초기화
		{
			GameObject newGearObj = new GameObject("Gear_" + data.itemID);
			newGearObj.transform.parent = player.transform;
			newGearObj.transform.localPosition = Vector3.zero;
			Gear targetGear = newGearObj.AddComponent<Gear>();
			targetGear.Init(data);
		}
		else
		{
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
	}
}