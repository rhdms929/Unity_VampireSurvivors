using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangAbility : IWeaponAbility
{
	private WeaponManager weaponManager;
	private float timer;

	public void Initialize(WeaponManager manager)
	{
		weaponManager = manager;
	}

	public void Execute()
	{

		if (weaponManager == null) return;

		timer += Time.deltaTime;

		if (timer > weaponManager.speed)
		{
			timer = 0;

			for (int i = 0; i < weaponManager.count; i++)
			{
				GameObject bolt = GameManager.instance.pool.Get(weaponManager.prefabId);

				// 부모 설정을 하지 않으므로 계층 구조가 깔끔하게 유지됨
				bolt.transform.position = weaponManager.player.transform.position + Vector3.up * 0.5f;

				float randomX = Random.Range(-0.7f, 0.7f);
				Vector3 launchDir = new Vector3(randomX, 1f, 0).normalized;

				// 부메랑 초기화 시 player 정보를 넘겨줍니다.
				if (bolt.TryGetComponent<Boomerang>(out Boomerang boomerang))
				{
					boomerang.Init(weaponManager.damage, 6f, launchDir, weaponManager.player.transform);
				}

				if (bolt.TryGetComponent<Weapon>(out Weapon weapon))
				{
					weapon.damage = weaponManager.damage;
					weapon.penetrate = -1;
				}
			}
		}
	}

	public void OnLevelUp() { }
}