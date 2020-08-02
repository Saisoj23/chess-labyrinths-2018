using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorController : UIUtils {

	public CanvasGroup[] groups;
	int indexLayout = 0;

	protected override void Awake ()
	{
		base.Awake();
		for (int i = 0; i < groups.Length; i++)
		{
			Button[] buttons = groups[i].GetComponentsInChildren<Button>();
			if (buttons.Length > 0) PlayerPrefs.SetInt("CountLevel", buttons.Length + (i * 5));
			for (int x = 0 + (i * 5); x < buttons.Length + (i * 5); x++)
			{
				if (!(PlayerPrefs.GetInt("LastLevel", 0) + 1 > x))
				{
					DisableButton(buttons[x - (i * 5)]);
				}
			}
		}
	}

	public void Next (bool prev)
	{
		int prevLayout;
		if (!prev)
		{
			prevLayout = indexLayout++;
			if (indexLayout > groups.Length - 1) 
			{
					indexLayout = 0;
			}
		} else
		{
			prevLayout = indexLayout--;
			if (indexLayout < 0) 
			{
					indexLayout = groups.Length - 1;
			}
		}
		StartCoroutine(Fade(groups[prevLayout], false, false));
		StartCoroutine(Fade(groups[indexLayout], true, false));
	}

	void DisableButton (Button button)
	{
		button.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
		button.enabled = false;
	}
}
