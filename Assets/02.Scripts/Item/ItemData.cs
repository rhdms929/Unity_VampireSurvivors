using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/ItemData")]

public class ItemData : ScriptableObject
{
	public enum ItemType
	{
		Melee,
		Ranged,
		Glove,
		Shoes,
		Health,
		Boomerang,
		Character
	}

	[Header("Main Item Info")]
	public ItemType itemType;
	public int itemID;
	public string itemName;

	[TextArea]
	public string itemDesc;
	public Sprite itemIcon;

	[Header("Level Data")]
	public float baseDamage;
	public int baseCount;
	public float baseSpeed;
	public float[] growthDamage;    // 레벨별 데미지 성장률
	public int[] growthCount;       // 레벨별 투사체 개수 성장

	[Header("Weapon Info")]
	public GameObject projectile;

	[Header("Character Selection Settings")]
	public int[] startWeaponIds;    // 캐릭터 시작 시 지급할 무기 ID 목록

	[Header("Character Stat Modifiers")]
	public float speedModifier = 1f;		// 이동속도 배율
	public float weaponSpeedModifier = 1f;	// 무기 속도 배율
	public float weaponRateModifier = 1f;	// 발사속도 배율
	public float damageModifier = 1f;		// 데미지 배율
	public int extraCount = 0;				// 추가 투사체 개수
}
