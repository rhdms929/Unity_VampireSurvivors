using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
	RectTransform rectTransform;
	ItemUpgrade[] items;


	void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		items = GetComponentsInChildren<ItemUpgrade>(true);
	}

	public void Show()
	{
		Next();
		rectTransform.localScale = Vector3.one;
		GameManager.instance.Stop();
	}

	public void Hide()
	{
		rectTransform.localScale = Vector3.zero;
		GameManager.instance.ReStart();
	}

	public void Select(int index)
	{
		items[index].OnClick();
	}

	void Next() // 랜덤 활성화 함수 작성
	{
		//	모든 아이템 비활성화
		foreach (ItemUpgrade item in items)
		{
			item.gameObject.SetActive(false);
		}
		//	3개 랜덤 활성화
		int[] rand = new int[3];
		while (true)
		{
			rand[0] = Random.Range(0, items.Length);
			rand[1] = Random.Range(0, items.Length);
			rand[2] = Random.Range(0, items.Length);

			if (rand[0] != rand[1] && rand[1] != rand[2] && rand[0] != rand[2])
				break;
		}

		for (int i = 0; i < rand.Length; i++)
		{
			ItemUpgrade randItem = items[rand[i]];
			//	만렙 아이템의 경우는 소비 아이템으로 대체
			if(randItem.level == randItem.data.growthDamage.Length)
			{
				items[4].gameObject.SetActive(true);
			}
			else
			{
				randItem.gameObject.SetActive(true);
			}
		}

	}
}
