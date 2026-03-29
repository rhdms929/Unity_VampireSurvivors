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
		AudioManager.instance.PlaySfx(AudioManager.SFX.LevelUp);
		AudioManager.instance.EffectBgm(true);
	}

	public void Hide()
	{
		rectTransform.localScale = Vector3.zero;
		GameManager.instance.ReStart();
		AudioManager.instance.PlaySfx(AudioManager.SFX.Select);
		AudioManager.instance.EffectBgm(false);
	}

	public void Select(int index)
	{
		items[index].OnClick();
	}

	void Next()
	{
		foreach (ItemUpgrade item in items)
		{
			item.gameObject.SetActive(false);
		}

		List<int> ranList = new List<int>();
		for (int i = 0; i < items.Length; i++)
		{
			ranList.Add(i);
		}

		// 리스트 셔플 
		for (int i = 0; i < ranList.Count; i++)
		{
			int rand = Random.Range(i, ranList.Count);
			int temp = ranList[i];
			ranList[i] = ranList[rand];
			ranList[rand] = temp;
		}

		for (int i = 0; i < 3; i++)
		{
			if (i >= ranList.Count) break;

			ItemUpgrade randItem = items[ranList[i]];

			// 만렙 아이템의 경우 소비 아이템)으로 대체
			if (randItem.level >= randItem.data.growthDamage.Length)
			{
				//  4번을 직접 켜는 게 아니라, 전체 아이템 중 Health 타입을 찾아 켭니다.
				foreach (ItemUpgrade item in items)
				{
					if (item.data.itemType == ItemData.ItemType.Health)
					{
						item.gameObject.SetActive(true);
						break; 
					}
				}
			}
			else
			{
				randItem.gameObject.SetActive(true);
			}
		}
	}
}
