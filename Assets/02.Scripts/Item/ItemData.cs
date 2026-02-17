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
		Boomerang
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
}
