using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IWeaponAbility
{
	// 초기 설정 (속도, 데미지 등)
	void Initialize(WeaponManager manager);
	// 매 프레임 실행될 로직 (회전 또는 발사 타이머)
	void Execute();
	// 레벨업 시 호출될 로직
	void OnLevelUp();
}