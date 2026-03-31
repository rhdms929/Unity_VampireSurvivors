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

	// 셔플 로직
	List<int> GetShuffledList(int count)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < count; i++)
			list.Add(i);

		for (int i = 0; i < list.Count; i++)
		{
			int rand = Random.Range(i, list.Count);
			int temp = list[i];
			list[i] = list[rand];
			list[rand] = temp;
		}
		return list;
	}

	// Health 아이템 찾기 분리
	ItemUpgrade GetHealthItem()
	{
		foreach (ItemUpgrade item in items)
		{
			if (item.data.itemType == ItemData.ItemType.Health)
				return item;
		}
		return null;
	}

	void Next()
	{
		foreach (ItemUpgrade item in items)
			item.gameObject.SetActive(false);

		List<int> ranList = GetShuffledList(items.Length);
		int shown = 0;

		for (int i = 0; i < ranList.Count && shown < 3; i++)
		{
			ItemUpgrade randItem = items[ranList[i]];

			if (randItem.level >= randItem.data.growthDamage.Length)
			{
				// 만렙 아이템 - Health 아이템으로 대체
				ItemUpgrade healthItem = GetHealthItem();
				if (healthItem != null && !healthItem.gameObject.activeSelf)
				{
					healthItem.gameObject.SetActive(true);
					shown++;
				}
			}
			else
			{
				randItem.gameObject.SetActive(true);
				shown++;
			}
		}
	}
}