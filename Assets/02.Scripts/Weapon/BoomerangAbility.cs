using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangAbility : IWeaponAbility
{
	private WeaponManager weaponManager;
	private float timer;

	const float LaunchSpeed = 6f;

	public void Initialize(WeaponManager manager)
	{
		weaponManager = manager;
		weaponManager.speed = 1.5f;
	}

	public void Execute()
	{
		if (weaponManager == null) return;

		timer += Time.deltaTime;
		if (timer > weaponManager.speed)
		{
			timer = 0f;
			FireBoomerangs();
		}
	}
	void FireBoomerangs()
	{
		for (int i = 0; i < weaponManager.count; i++)
		{
			GameObject bolt = GameManager.instance.pool.Get(weaponManager.prefabId);
			bolt.transform.position = weaponManager.player.transform.position + Vector3.up * 0.5f;

			float randomX = Random.Range(-0.7f, 0.7f);
			Vector3 launchDir = new Vector3(randomX, 1f, 0).normalized;

			if (bolt.TryGetComponent(out Boomerang boomerang))
				boomerang.Init(weaponManager.damage, LaunchSpeed, launchDir, weaponManager.player.transform);

			if (bolt.TryGetComponent(out Weapon weapon))
			{
				weapon.damage = weaponManager.damage;
				weapon.penetrate = -1;
			}
		}
	}

	public void OnLevelUp() { }
}