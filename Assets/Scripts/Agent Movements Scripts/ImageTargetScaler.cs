using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTargetScaler : MonoBehaviour
{
    public GameObject lowScaleObject;

	void Update()
	{
		if (lowScaleObject.activeSelf)
		{
			this.gameObject.SetActive(true);
			transform.localPosition = lowScaleObject.transform.localPosition * 100f;
			transform.localRotation = lowScaleObject.transform.localRotation;
		}
		else
		{
			this.gameObject.SetActive(false);
		}
	}
}