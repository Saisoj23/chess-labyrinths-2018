using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : UIUtils {
	
	public int levelNumber;

	public static int maxlevel;
	public float maxZoom = 3;

	public float gestureZoomSpeed;
	public float whellZoomSpeed;

	Canvas canvas;
	CanvasGroup canvasGroup;
	public Camera cam;
	Slider slider;

	override protected void Awake ()
	{
		base.Awake();
		slider = GetComponentInChildren<Slider>();
		canvasGroup = GameObject.Find("PauseCanvas").GetComponent<CanvasGroup>();
		cam = GameObject.FindObjectOfType<Camera>();
		canvas = GetComponent<Canvas>();
		int.TryParse(GetSceneName().Substring(5), out levelNumber);
		maxlevel = PlayerPrefs.GetInt("CountLevel", 1);
		GameObject.Find("Level").GetComponent<TMPro.TMP_Text>().text = "Level " + levelNumber;
		GameObject.FindObjectOfType<Light>().shadows = PlayerPrefs.GetInt("Shadows", 1) == 0 ? LightShadows.None : LightShadows.Hard;
		slider.value = PlayerPrefs.GetFloat("Level" + levelNumber + "ZoomValue", 1f);
		ChangeCamera(cam);
		Debug.Log("Awake de Pause");
	}

	override protected void Start ()
	{
		base.Start();
		Debug.Log("Start de Pause");
		if (dontDestroy != null) dontDestroy.Hide(true);
	}

	void Update ()
	{
		#if !UNITY_ANDROID || UNITY_EDITOR
		slider.value += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * whellZoomSpeed;
		#endif

		#if UNITY_ANDROID
		if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            slider.value -= deltaMagnitudeDiff * gestureZoomSpeed;
		}
		#endif
	}

	public void ChangeZoom ()
	{
		cam.orthographicSize = Mathf.Lerp(maxZoom, 2.5f, slider.value);
	}

	public void ChangeCamera (Camera camera)
	{
		cam = camera;
		canvas.worldCamera = camera;
		canvas.planeDistance = 1;
	}

	public void YouWin ()
	{
		GameObject.Find("Level").GetComponent<TMPro.TMP_Text>().text = "You Win!";
		ActiveButton(GameObject.Find("Play").GetComponent<Button>(), false);
		ActiveButton(GameObject.Find("Play2").GetComponent<Button>(), true);
		PlayerPrefs.SetFloat("Level" + levelNumber + "ZoomValue", slider.value);
		Pause();
		if (levelNumber > PlayerPrefs.GetInt("LastLevel", 0))
		{
			PlayerPrefs.SetInt("LastLevel", levelNumber);
		}
	}

	public void YouLose ()
	{
		GameObject.Find("Level").GetComponent<TMPro.TMP_Text>().text = "You Lose!";
		ActiveButton(GameObject.Find("Play").GetComponent<Button>(), false);
		ActiveButton(GameObject.Find("Restar").GetComponent<Button>(), false);
		ActiveButton(GameObject.Find("Restar2").GetComponent<Button>(), true);
		PlayerPrefs.SetFloat("Level" + levelNumber + "ZoomValue", slider.value);
		Pause();
	}

	public void Pause ()
	{	
		if (Time.timeScale == 0)
		{
			Time.timeScale = 1f;
			if (dontDestroy != null)
			{
				dontDestroy.Hide(true);
			}
			StartCoroutine(Fade(canvasGroup, false, false));
		}
		else if (Time.timeScale == 1)
		{
			if (dontDestroy != null)
			{
				dontDestroy.Hide(false);
			}
			StartCoroutine(Fade(canvasGroup, true, true));
		}
	}

	public void Reestar ()
	{
		GoToScene("Level" + levelNumber);
	}

	public void NextLevel ()
	{
		if ((levelNumber + 1) <= maxlevel)
		{
			GoToScene("Level" + (levelNumber + 1));
		} else {
			GoToScene("Credits");
		}
	}
}