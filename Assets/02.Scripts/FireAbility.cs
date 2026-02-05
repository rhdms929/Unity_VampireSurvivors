using UnityEngine;

public class FireAbility : IWeaponAbility
{
	private WeaponManager _manager;
	private float _timer;

	public void Initialize(WeaponManager manager)
	{
		_manager = manager;
		_manager.speed = 0.3f;
	}

	public void Execute()
	{
		_timer += Time.deltaTime;
		if (_timer > _manager.speed)
		{
			_timer = 0f;
			Fire();
		}
	}

	public void OnLevelUp() { /* 발사형은 레벨업 시 특별한 즉시 실행 로직 없음 */ }

	private void Fire()
	{
		if (!_manager.player.scanner.nearestTarget) return;

		Vector3 targetPos = _manager.player.scanner.nearestTarget.position;
		Vector3 dir = (targetPos - _manager.transform.position).normalized;

		Transform bullet = GameManager.instance.pool.Get(_manager.prefabId).transform;
		bullet.position = _manager.transform.position;
		bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
		bullet.GetComponent<Weapon>().Init(_manager.damage, _manager.count, dir);
	}
}