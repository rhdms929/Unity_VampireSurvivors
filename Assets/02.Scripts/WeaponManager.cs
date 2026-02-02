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
				break;
		}

		//Test
		if (Input.GetKeyDown(KeyCode.Space))
		{
			LevelUp(20, 5);
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
			weapon.GetComponent<Weapon>().Init(damage, -1); //	-1은 무한으로 공격하는 근접공격
		}
	}

}
