using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : UIUtils {

	public Image image;
	public Sprite[] images;

	protected override void Awake ()
	{
		base.Awake();
		if (PlayerPrefs.GetInt("LastLevel", 0) == 0) image.overrideSprite = images[0];
		else image.overrideSprite = images[Random.Range(0, 5)];
	}
}
