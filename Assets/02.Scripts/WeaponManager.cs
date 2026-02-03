using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	public int id;
	public int prefabId;
	public float damage;
	public int count;
	public float speed;

	float timer;
	Player player;

	void Awake()
	{
		player = GetComponentInParent<Player>();
	}

	void Start()
	{
		Init();
	}

	void Update()
	{
		switch (id)
		{
			case 0:
				transform.Rotate(Vector3.back * speed * Time.deltaTime);
				break;
			default:
				timer += Time.deltaTime;
				if (timer > speed)
				{
					timer = 0f;
					Fire();
				}
				break;
		}

		//Test
		if (Input.GetKeyDown(KeyCode.Space))
		{
			LevelUp(10, 1);
		}
	}

	public void LevelUp(float damage, int count)
	{
		this.damage = damage;
		this.count += count;

		if(id == 0)
			Batch();
	}


	public void Init()
	{
		switch(id)
		{
			case 0:
				speed = 150;
				Batch();
				break;
			default:
				speed = 0.3f;
				break;
		}
	}

	void Batch()
	{
		for(int i = 0; i < count; i++)
		{
			Transform weapon;

			if ( i < transform.childCount)
			{
				weapon = transform.GetChild(i);
			}
			else
			{
				weapon = GameManager.instance.pool.Get(prefabId).transform;
				weapon.parent = transform;
			}
			

			weapon.localPosition = Vector3.zero;
			weapon.localRotation = Quaternion.identity;

			Vector3 rotVec = Vector3.forward * 360 * i / count;
			weapon.Rotate(rotVec);
			weapon.Translate(weapon.up * 1.5f, Space.World);
			weapon.GetComponent<Weapon>().Init(damage, -1, Vector3.zero); //	-1은 무한으로 공격하는 근접공격
		}
	}

	void Fire()
	{
		if (!player.scanner.nearestTarget)
			return;

		Vector3 targetPos = player.scanner.nearestTarget.position;
		Vector3 dir = (targetPos - transform.position).normalized;

		Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
		bullet.position = transform.position;
		bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
		bullet.GetComponent<Weapon>().Init(damage, count, dir);
	}

}
