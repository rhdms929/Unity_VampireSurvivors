using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
	public float scanRange;
	public LayerMask targetLayer;
	public Collider2D[] targets;
	public Transform nearestTarget;

	void FixedUpdate()
	{
		targets = Physics2D.OverlapCircleAll(transform.position, scanRange, targetLayer);
		nearestTarget = GetNearestTarget();
	}

	Transform GetNearestTarget()
	{
		Transform nearest = null;
		float minDistance = float.MaxValue;
		foreach (Collider2D target in targets)
		{
			float distance = Vector2.Distance(transform.position, target.transform.position);
			if (distance < minDistance)
			{
				minDistance = distance;
				nearest = target.transform;
			}
		}
		return nearest;
	}
}
