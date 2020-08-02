using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour {

	//public List<Color> colors = new List<Color>();
	public float moveSpeed = 0.05f;

	protected Camera mcamera;
	protected Rigidbody rb;
	protected LevelController level;
	Animator anim;
	AudioListener listener;

	protected Vector3 finalPosition;
	Vector3 velocity;
	PauseController pause;
	protected Vector3[] direccions; 

	void Awake () 
	{
		mcamera = GetComponentInChildren<Camera>();
		level = GameObject.FindGameObjectWithTag("Pause").GetComponent<LevelController>();
		pause = level.GetComponent<PauseController>();
		anim = GetComponent<Animator>();
		listener = GetComponent<AudioListener>();
		
		finalPosition = transform.position;
		SetDireccions();
	}

	public abstract void StartPlayerTurn ();
	
	protected virtual void SetDireccions()
	{
		direccions = new Vector3[4] {Vector3.left, Vector3.right, Vector3.forward, Vector3.back};
	}

	protected virtual void Move() 
	{	
		level.DesactiveFloor();
		StartCoroutine(MoveCouritine());
	}

	public virtual void FinalMove()
	{
		level.StartNewTurn();
	}

	protected FloorController Raycasting (Ray ray, out RaycastHit hit) 
	{
		if (Physics.Raycast(ray, out hit, 1.5f, LevelController.rayBoxLayer))
		{
			FloorController floor = hit.collider.GetComponent<FloorController>();
			if (!floor.bloqued)
				return floor;
			else return null;
		}
		else return null;
	}

	public IEnumerator MoveCouritine () 
	{
		Ray ray = new Ray(transform.position + (Vector3.up * 0.3f), Vector3.Normalize(-(transform.position - finalPosition)));
		RaycastHit[] hits;
		//Debug.DrawRay(ray.origin, ray.direction, Color.red, 5f);
		ray.origin -= ray.direction;
		hits = Physics.RaycastAll(ray, Vector3.Distance(transform.position, finalPosition), LevelController.rayBoxLayer);
		while (Vector3.Distance(transform.position, finalPosition) > 0.05)
		{
			transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref velocity, moveSpeed);
			yield return null;
		}
		transform.position = finalPosition;
		for (int i = 0; i < hits.Length; i++)
		{
			FloorController floor = hits[i].collider.gameObject.GetComponent<FloorController>();
			if (floor.afiliedObject != null)
			{
				DamageController thisDamage = floor.afiliedObject.GetComponent<DamageController>();
				if (thisDamage != null)
					thisDamage.Colides();
			}
		}
		Colides(true);
		FinalMove();
	}

	public void Colides (bool first = false) 
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.up, out hit, 1, LevelController.rayBoxLayer))
		{
			Debug.DrawRay(transform.position, Vector3.up, Color.red, 4f);
			FloorController floor = hit.collider.gameObject.GetComponentInParent<FloorController>();
			if (floor != null)
			{
				switch (floor.tipe) 
				{
				case "Damage":
					anim.SetTrigger("GameOver");
					level.GameOver();
					break;
					
				case "End":
					anim.SetTrigger("Win");
					floor.transform.parent.GetComponent<Animator>().SetTrigger("Final");
					level.YouWin();
					break;
				
				case "Change":
					PlayerController newPlayer = Instantiate(floor.afiliedObject.GetComponent<ChangePlayer>().player, transform.position, transform.rotation).GetComponent<PlayerController>();
					pause.ChangeCamera(newPlayer.mcamera);
					pause.ChangeZoom();
					level.player = newPlayer;
					Destroy(floor.afiliedObject);
					floor.afiliedObject = null;
					floor.tipe = "";
					floor.stopHere = false;
					anim.SetTrigger("GameOver");
					anim.speed *= 3;
					GetComponentInChildren<AudioListener>().enabled = false;
					Destroy(gameObject, 2f);
					this.enabled = false;
					break;

				case "Button":
					if (first)
					{
						floor.afiliedObject.GetComponent<ButtonController>().UpdateAnimation();
					}
					break;
				}
			}
		}
	}
	
	void Update () 
	{
		#if !UNITY_ANDROID || UNITY_EDITOR
		if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f)
		{
			RaycastHit hit;
			Ray ray = mcamera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100f, LevelController.clickeableLayer))
			{
				finalPosition = hit.collider.transform.parent.transform.parent.transform.position + (Vector3.up * 0.5f);
				Move();
			}
		}
		#endif

		#if UNITY_ANDROID
		if (Input.GetMouseButtonDown(0) && Time.timeScale != 0f && Input.touchCount == 1)
		{
			RaycastHit hit;
			Ray ray = mcamera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100f, LevelController.clickeableLayer))
			{
				finalPosition = hit.collider.transform.parent.transform.parent.transform.position + (Vector3.up * 0.5f);
				Move();
			}
		}
		#endif
	}
}
