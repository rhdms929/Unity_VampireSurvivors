using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rectTransform;	//	UI 위치, 크기
	void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
	}

	void FixedUpdate()
	{
		rectTransform.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
	}
}

