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
	public float[] growthDamage;
	public int[] growthCount;

	[Header("Weapon Info")]
	public GameObject projectile;

	[Header("Character Selection Settings")]
	public int startWeaponId;

	[Header("Character Stat Modifiers")]
	public float speedModifier = 1f;       // 기본 1배 (1.1f면 10% 증가)
	public float weaponSpeedModifier = 1f;
	public float weaponRateModifier = 1f;
	public float damageModifier = 1f;      // 1.2f면 20% 증가
	public int extraCount = 0;             // 추가 투사체 개수
}
