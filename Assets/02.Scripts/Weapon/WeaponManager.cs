using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	public enum WeaponType { Orbit, Fire , Boomerang }
	public WeaponType type;

	public ItemData data;
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
		damage = data.baseDamage * Character.Damage;
		count = data.baseCount + Character.Count;
		this.data = data;

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
		else if (data.itemType == ItemData.ItemType.Boomerang) type = WeaponType.Boomerang;

		switch (type)
		{
			case WeaponType.Orbit: _ability = new Orbit(); 
				break;
			case WeaponType.Fire: _ability = new FireAbility();
				speed = 0.5f;
				break;
			case WeaponType.Boomerang: _ability = new BoomerangAbility();
				speed = 1.5f;
				break;
		}

		_ability.Initialize(this);
	}

	void Update()
	{
		if (!GameManager.instance.isLive)
			return;

		_ability?.Execute();

	}

	public void LevelUp(float damage, int count)
	{
		this.damage = damage * Character.Damage;
		this.count += count;
		_ability.OnLevelUp();

		player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
	}
}
