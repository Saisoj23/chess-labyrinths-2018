using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIUtils : MonoBehaviour {

	public float fadeSpeed = 7;

	public bool down;

	protected DontDestroyOnLoad dontDestroy;

	virtual protected void Awake ()
	{
		Debug.Log("Awake de UI Utils");
		dontDestroy = GameObject.FindObjectOfType<DontDestroyOnLoad>();
	}

	virtual protected void Start ()
	{
		Debug.Log("Start de UI Utils");
		if (dontDestroy != null) dontDestroy.UpdatePosition(down);
	}

	public void ActiveButton (Button button, bool active)
	{
		button.enabled = active;
		button.GetComponent<Image>().enabled = active;
		Text text = button.GetComponentInChildren<Text>();
		if (text != null)
		{
			text.enabled = active;
		}
	}

	public void GoToScene(string sceneName)
	{
		if (Time.timeScale != 1) Time.timeScale = 1;
		SceneManager.LoadScene(sceneName);
	}

	public string GetSceneName()
	{
		return SceneManager.GetActiveScene().name;
	}

	public void ExitGame()
	{
		dontDestroy.Destroy();
		#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	public void ReestarProgres() {PlayerPrefs.DeleteAll();}

	protected IEnumerator Fade (CanvasGroup canvas, bool fade, bool stopTime)
	{
		if (fade)
		{
			canvas.blocksRaycasts = true;
			canvas.interactable = true;
			for (float fadeValue = 0; fadeValue < 1; fadeValue += Time.deltaTime * fadeSpeed)
			{
				canvas.alpha = fadeValue;
				yield return null;
			}
			canvas.alpha = 1f;
		} else
		{
			canvas.blocksRaycasts = false;
			canvas.interactable = false;
			for (float fadeValue = 1; fadeValue > 0; fadeValue -= Time.deltaTime * fadeSpeed)
			{
				canvas.alpha = fadeValue;
				yield return null;
			}
			canvas.alpha = 0f;
		}
		if (stopTime)
		{
			Time.timeScale = 0f;
		}
	}
}
