using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	// 이제 playerId가 0인지 1인지 체크할 필요가 없습니다.
	// 데이터 파일(ItemData)에 적힌 숫자를 그대로 가져오기 때문입니다.

	public static float Speed => GameManager.instance.SelectedCharacterData.speedModifier;

	public static float WeaponSpeed => GameManager.instance.SelectedCharacterData.weaponSpeedModifier;

	public static float WeaponRate => GameManager.instance.SelectedCharacterData.weaponRateModifier;

	public static float Damage => GameManager.instance.SelectedCharacterData.damageModifier;

	public static int Count => GameManager.instance.SelectedCharacterData.extraCount;
}
