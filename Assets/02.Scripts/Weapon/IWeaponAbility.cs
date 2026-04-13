using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IWeaponAbility
{
	void Initialize(WeaponManager manager);		// 초기 설정 (속도, 데미지 등)
	void Execute();								// 매 프레임 실행될 로직 
	void OnLevelUp();                           // 레벨업 시 호출될 로직
}