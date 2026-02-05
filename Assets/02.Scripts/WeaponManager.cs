using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	public enum WeaponType { Orbit, Fire }
	public WeaponType type;

	public int prefabId;
	public float damage;
	public int count;
	public float speed;

	public Player player { get; private set; }
	private IWeaponAbility _ability;

	void Awake()
	{
		player = GetComponentInParent<Player>();
	}

	void Start()
	{
		// 무기 타입에 따라 전략 주입
		SetStrategy();
	}

	void SetStrategy()
	{
		switch (type)
		{
			case WeaponType.Orbit:
				_ability = new Orbit();
				break;
			case WeaponType.Fire:
				_ability = new FireAbility();
				break;
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
	}
}
