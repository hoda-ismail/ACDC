using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconPressChanger : MonoBehaviour, IPointerDownHandler
{
	public Sprite pressed, released;

	private Button btn;

	void Start()
	{
		btn = this.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
		GetComponent<Image>().sprite = released;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		GetComponent<Image>().sprite = pressed;
	}

}