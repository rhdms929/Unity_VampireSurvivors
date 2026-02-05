using UnityEngine;

public class Orbit : IWeaponAbility
{
	private WeaponManager _manager;

	public void Initialize(WeaponManager manager)
	{
		_manager = manager;
		_manager.speed = 150f;
		Batch();
	}

	public void Execute()
	{
		// 중심축(WeaponManager)을 회전시킴
		_manager.transform.Rotate(Vector3.back * _manager.speed * Time.deltaTime);
	}

	public void OnLevelUp()
	{
		Batch();
	}

	private void Batch()
	{
		for (int i = 0; i < _manager.count; i++)
		{
			Transform weapon;
			if (i < _manager.transform.childCount)
			{
				weapon = _manager.transform.GetChild(i);
			}
			else
			{
				weapon = GameManager.instance.pool.Get(_manager.prefabId).transform;
				weapon.parent = _manager.transform;
			}

			weapon.localPosition = Vector3.zero;
			weapon.localRotation = Quaternion.identity;

			Vector3 rotVec = Vector3.forward * 360 * i / _manager.count;
			weapon.Rotate(rotVec);
			weapon.Translate(weapon.up * 1.5f, Space.World);

			// 근접 공격용 초기화 (-1)
			weapon.GetComponent<Weapon>().Init(_manager.damage, -1, Vector3.zero);
		}
	}
}