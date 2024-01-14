using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class DontDestroyOnLoad : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{

	string gameId = "3744355";
	string video = "video";

	bool _testMode = true;

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
		
		#if UNITY_ANDROID
		_testMode = false;
		#endif

		InitializeAds();
		nextAd = Time.time + 300;
	}

	public void TryAd()
	{
		#if UNITY_ANDROID || UNITY_EDITOR
		if (Time.time > nextAd && !Advertisement.isShowing)
		{
			nextAd = Time.time + 300; 
			#if UNITY_ANDROID || UNITY_EDITOR
			ShowAd();
			#endif
		}
		#else
		return;
		#endif
	}

	public void InitializeAds()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, _testMode, this);
        }
    }

	public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Advertisement.Load(video, this);
    }
 
    // Show the loaded content in the Ad Unit:
    public void ShowAd()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Advertisement.Show(video, this);
    }

	public void OnInitializationComplete()
    {
		LoadAd();
        Debug.Log("Unity Ads initialization complete.");
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

	public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
    }
 
    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }
 
    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }
 
    public void OnUnityAdsShowStart(string _adUnitId) { }
    public void OnUnityAdsShowClick(string _adUnitId) { }
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) 
	{
		LoadAd();
	}
}
