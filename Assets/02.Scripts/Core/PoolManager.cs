using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	//	프리팹들을 보관할 변수
	public GameObject[] prefabs;

	//	풀 담당을 하는 리스트들
	List<GameObject>[] pools;

	void Awake()
	{
		//	풀 리스트들 초기화
		pools = new List<GameObject>[prefabs.Length];

		for (int i = 0; i < pools.Length; i++)
		{
			pools[i] = new List<GameObject>();
		}
	}

	public GameObject Get(int prefabIndex)
	{
		GameObject select = null;

		// 선택한 풀의 놀고 있는 게임오브젝트 접근

		foreach (GameObject item in pools[prefabIndex])
		{
			if (!item.activeSelf)
			{
				//	발견하면 select에 할당 후 활성화
				select = item;
				select.SetActive(true);     // 비활성화 오브젝트를 찾으면 SetActive(true)로 활성화
				break;
			}
		}

		//	못 찾았으면? 새롭게 생성하고 select에 할당
		if(!select)
		{
			select = Instantiate(prefabs[prefabIndex], transform);
			pools[prefabIndex].Add(select);
		}

		return select;

	}

}
