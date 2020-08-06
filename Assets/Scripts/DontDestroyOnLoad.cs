using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID || UNITY_EDITOR
using UnityEngine.Advertisements;
#endif

public class DontDestroyOnLoad : MonoBehaviour {

	string gameId = "3744355";
	string video = "video";

	float nextAd;

	public static DontDestroyOnLoad dontDestroy;

	void Awake ()
	{
		if (dontDestroy == null)
		{
			dontDestroy = this;
			DontDestroyOnLoad(gameObject);
		} else if (dontDestroy != this)
		{
			Destroy(gameObject);
		}
		GetComponent<AudioSource>().enabled = PlayerPrefs.GetInt("Music", 1) == 0 ? false : true;
		#if UNITY_EDITOR 
		Advertisement.Initialize(gameId, true);
		#elif UNITY_ANDROID
		Advertisement.Initialize(gameId, false);
		#endif
		nextAd = Time.time + 300;
	}

	public void TryAd()
	{
		if (Time.time > nextAd && Advertisement.IsReady() && !Advertisement.isShowing)
		{
			nextAd = Time.time + 300; 
			#if UNITY_ANDROID || UNITY_EDITOR
			Advertisement.Show(video);
			#endif
		}
	}
}
