using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	FloorController[] floor;
	DamageController[] damage;
	public PlayerController player;
	PauseController pause;
	AudioSource sound;

	public AudioClip winClip;
	public AudioClip loseClip;
	public AudioClip moveClip;

	public static int clickeableLayer = (1 << 10);
	public static int rayBoxLayer = (1 << 9);

	public bool gameOver = false;
	void Awake () 
	{
		floor = GameObject.FindObjectsOfType<FloorController>();
		damage = GameObject.FindObjectsOfType<DamageController>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		pause = GameObject.FindGameObjectWithTag("Pause").GetComponent<PauseController>();
		sound = GetComponent<AudioSource>();
		sound.enabled = PlayerPrefs.GetInt("Sound", 1) == 0 ? false : true;
		
	}

	void Start ()
	{
		ActiveFloor();
	}

	public void StartNewTurn ()
	{
		if (!gameOver)
		{
			for (int i = 0; i < damage.Length; i++)
			{
				damage[i].StartTurnDamage();
			}
			Invoke("ActiveFloor", 0.5f);
			Invoke("CheckColides", 0.3f);
		}
	}

	void CheckColides ()
	{
		player.Colides(false);
	}
	
	public void ActiveFloor () {

		for (int i = 0; i < floor.Length; i++) 
		{
			floor[i].Move(Vector3.zero, true);
			floor[i].Active(false);
		}
		player.StartPlayerTurn();
	}

	public void DesactiveFloor () {
		sound.clip = moveClip;
		sound.Play();
		for (int i = 0; i < floor.Length; i++)
		{
			floor[i].Active(false);
		}
	}

	public void GameOver () 
	{
		sound.clip = loseClip;
		sound.Play();
		gameOver = true;
		CancelMovement();
		pause.Invoke("YouLose", 2f);

	}
	public void YouWin ()
	{
		sound.clip = winClip;
		sound.Play();
		gameOver = true;
		CancelMovement();
		pause.Invoke("YouWin", 2f);
	}

	void CancelMovement ()
	{
		player.enabled = false;
		for (int i = 0; i < damage.Length; i++)
		{
			damage[i].enabled = false;
		}
		for (int i = 0; i < floor.Length; i++) 
		{
			floor[i].enabled = false;
		}
	}
}
