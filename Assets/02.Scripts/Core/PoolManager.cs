using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	//	프리팹들을 보관할 변수
	public GameObject[] prefabs;

	//	풀 담당을 하는 리스트들
	Queue<GameObject>[] pools;

	// 프리팹 인덱스 역매핑 (오브젝트 -> 인덱스)
	Dictionary<GameObject, int> prefabIndexMap;

	void Awake()
	{
		//	풀 리스트들 초기화
		pools = new Queue<GameObject>[prefabs.Length];

		for (int i = 0; i < pools.Length; i++)
		{
			pools[i] = new Queue<GameObject>();
			prefabIndexMap = new Dictionary<GameObject, int>();
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
	// 오브젝트를 풀에 반환
	public void Return(GameObject obj, int prefabIndex)
	{
		obj.SetActive(false);
		pools[prefabIndex].Enqueue(obj);
	}

	// 프리팹 레퍼런스로도 반환 가능 (편의 오버로드)
	public void Return(GameObject obj, GameObject prefab)
	{
		if (prefabIndexMap.TryGetValue(prefab, out int index))
		{
			Return(obj, index);
		}
		else
		{
			Debug.LogWarning($"[PoolManager] 등록되지 않은 프리팹입니다: {prefab.name}");
		}
	}

}
