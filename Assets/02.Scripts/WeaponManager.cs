using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	public enum WeaponType { Orbit, Fire }
	public WeaponType type;

	public int id;
	public int prefabId;
	public float damage;
	public int count;
	public float speed;

	public Player player { get; private set; }
	private IWeaponAbility _ability;

	void Awake()
	{
		player = GameManager.instance.player;
	}

	public void SetStrategy(ItemData data)
	{
		if (player == null) player = GetComponentInParent<Player>();

		id = data.itemID;
		damage = data.baseDamage;
		count = data.baseCount;

		for (int i = 0; i < GameManager.instance.pool.prefabs.Length; i++)
		{
			if (data.projectile == GameManager.instance.pool.prefabs[i])
			{
				prefabId = i;
				break;
			}
		}

		if (data.itemType == ItemData.ItemType.Melee) type = WeaponType.Orbit;
		else if (data.itemType == ItemData.ItemType.Ranged) type = WeaponType.Fire;

		switch (type)
		{
			case WeaponType.Orbit: _ability = new Orbit(); break;
			case WeaponType.Fire: _ability = new FireAbility(); break;
		}

		_ability.Initialize(this);
	}

	void Update()
	{
		_ability?.Execute();

		if (Input.GetKeyDown(KeyCode.Space))
		{
			LevelUp(10, 1);
		}
	}

	public void LevelUp(float damage, int count)
	{
		this.damage = damage;
		this.count += count;
		_ability.OnLevelUp();

		player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
	}
}
