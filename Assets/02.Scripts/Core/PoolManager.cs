using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	public GameObject[] prefabs;
	Queue<GameObject>[] pools;
	Dictionary<GameObject, int> prefabIndexMap;

	void Awake()
	{
		pools = new Queue<GameObject>[prefabs.Length];
		prefabIndexMap = new Dictionary<GameObject, int>(); 

		for (int i = 0; i < pools.Length; i++)
		{
			pools[i] = new Queue<GameObject>();
			prefabIndexMap[prefabs[i]] = i; 
		}
	}

	public GameObject Get(int prefabIndex)
	{
		GameObject select;

		// 풀에 여유 오브젝트가 있으면 꺼내서 활성화
		if (pools[prefabIndex].Count > 0)
		{
			select = pools[prefabIndex].Dequeue();
			select.SetActive(true);
		}
		else
		{
			// 없으면 새로 생성 후 반환
			select = Instantiate(prefabs[prefabIndex], transform);
		}

		return select;

	} 
	public void Return(GameObject obj, int prefabIndex)
	{
		obj.SetActive(false);
		pools[prefabIndex].Enqueue(obj);
	}

	public void Return(GameObject obj, GameObject prefab)
	{
		if (prefabIndexMap.TryGetValue(prefab, out int index))
		{
			Return(obj, index);
		}
	}

}
