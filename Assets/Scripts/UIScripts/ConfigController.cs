using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class ConfigController : UIUtils {

	public Toggle music;
	public Toggle sound;
	public Toggle bloom;
	public Toggle vignette;
	public Toggle shadows;

	PostProcessingBehaviour post;
	AudioSource audioSource;

	protected override void Awake ()
	{
		base.Awake();
		post = GameObject.FindObjectOfType<PostProcessingBehaviour>();
		audioSource = GameObject.FindObjectOfType<AudioSource>();

		music.isOn = PlayerPrefs.GetInt("Music", 1) == 0 ? false : true;
		sound.isOn = PlayerPrefs.GetInt("Sound", 1) == 0 ? false : true;
		bloom.isOn = PlayerPrefs.GetInt("Bloom", 1) == 0 ? false : true;
		vignette.isOn = PlayerPrefs.GetInt("Vignette", 1) == 0 ? false : true;
		shadows.isOn = PlayerPrefs.GetInt("Shadows", 1) == 0 ? false : true;
	}

	public void SetMusic () 
	{
		PlayerPrefs.SetInt("Music", music.isOn ? 1 : 0);
		audioSource.enabled = music.isOn;
	}
	
	public void SetSoundEffects () {PlayerPrefs.SetInt("Sound", sound.isOn ? 1 : 0);}

	public void SetBloom () 
	{
		PlayerPrefs.SetInt("Bloom", bloom.isOn ? 1 : 0);
		post.profile.bloom.enabled = bloom.isOn;
	}

	public void SetVignette () 
	{
		PlayerPrefs.SetInt("Vignette", vignette.isOn ? 1 : 0);
		post.profile.vignette.enabled = vignette.isOn;
	}

	public void SetShadows () {PlayerPrefs.SetInt("Shadows", shadows.isOn ? 1 : 0);}
}
