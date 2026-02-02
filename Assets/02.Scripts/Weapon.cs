using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public float damage;
	public int penetrate;

	public void Init(float damage, int penetrate)
	{
		this.damage = damage;
		this.penetrate = penetrate;
	}
}
