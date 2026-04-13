using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public static float Speed => GameManager.instance?.SelectedCharacterData?.speedModifier ?? 1f;
	public static float WeaponSpeed => GameManager.instance?.SelectedCharacterData?.weaponSpeedModifier ?? 1f;
	public static float WeaponRate => GameManager.instance?.SelectedCharacterData?.weaponRateModifier ?? 1f;
	public static float Damage => GameManager.instance?.SelectedCharacterData?.damageModifier ?? 1f;
	public static int Count => GameManager.instance?.SelectedCharacterData?.extraCount ?? 0;
}
