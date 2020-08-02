using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour {

	Vector3 startPosition;

	public bool stopHere;
	public bool bloqued;
	public string tipe;
	public GameObject afiliedObject;

	Animator anim;
	BoxCollider mesh;
	BoxCollider box;

	void Awake () 
	{
		anim = GetComponent<Animator>();
		mesh = GetComponentInChildren<MeshRenderer>().GetComponent<BoxCollider>();
		Debug.Log(mesh.gameObject.name);
		box = GetComponent<BoxCollider>();

		startPosition = mesh.transform.position;
	}
	public void Active (bool active) 
	{
		anim.SetBool("Blink", active);
		mesh.enabled = active;
	}

	public void ActiveBox (bool active)
	{
		box.enabled = active;
	}

	public void Move (Vector3 position, bool start)
	{
		if (start) mesh.transform.position = startPosition;
		else mesh.transform.position = new Vector3(position.x, startPosition.y, position.z);
	}
}
