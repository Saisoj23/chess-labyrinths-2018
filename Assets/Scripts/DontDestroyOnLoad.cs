using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GoogleMobileAds.Api;

public class DontDestroyOnLoad : MonoBehaviour {

	public static DontDestroyOnLoad dontDestroy;
	/*BannerView bannerView;
	AdRequest request;*/

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

		/*#if UNITY_ANDROID
            string appId = "ca-app-pub-6408372397348107~9217223397";
        #else
            string appId = "unexpected_platform";
        #endif
		MobileAds.Initialize(appId);

		#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-6408372397348107/7900272294";
        #else
            string adUnitId = "unexpected_platform";
        #endif
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);*/
	}

	void Start()
	{
		RequestBanner();
	}

	public void RequestBanner()
    {
		/*request = new AdRequest.Builder().AddTestDevice("FC20B80661C8C7C7DCE1F2B230F541BE").Build();
        bannerView.LoadAd(request);*/
		Invoke("RequestBanner", 60f);
    }

	public void UpdatePosition (bool down)
	{
		/*bannerView.SetPosition(down ? AdPosition.Bottom : AdPosition.Top);*/
	}

	public void Hide(bool hide)
	{
		/*if (!hide) bannerView.Show();
		else bannerView.Hide();*/
	}

	public void Destroy()
	{
		/*bannerView.Destroy();*/
	}
}
