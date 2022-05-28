using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TakeScreenShot : MonoBehaviour
{
	public Animator blink;
	public string blinkCondition;

	public void TakeAScreenShot()
	{
		StartCoroutine(Capture());
	}

	private IEnumerator Capture()
	{
		string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
		yield return new WaitForEndOfFrame();
		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();

		string filePath = Path.Combine(GetAndroidExternalStoragePath(), "Tanks" + timeStamp + ".png");
		File.WriteAllBytes(filePath, ss.EncodeToPNG());
		blink.Play(blinkCondition, 0, 0);
		//Handheld.Vibrate();
	}

	private string GetAndroidExternalStoragePath()
	{
		if (Application.platform != RuntimePlatform.Android)
			return Application.persistentDataPath;

		var jc = new AndroidJavaClass("android.os.Environment");
		var path = jc.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory",
			jc.GetStatic<string>("DIRECTORY_DCIM"))
			.Call<string>("getAbsolutePath");
		return path;
	}

}