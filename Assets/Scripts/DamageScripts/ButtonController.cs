using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

	Animator[] cubes;
	FloorController[] floors;
	Animator anim;
	public bool active;

	void Awake ()
	{
		anim = GetComponent<Animator>();
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.up, out hit, 1, LevelController.rayBoxLayer))
		{
			FloorController floor = hit.collider.gameObject.GetComponent<FloorController>();
			floor.afiliedObject = gameObject;
			floor.tipe = "Button";
			floor.stopHere = true;
		}
		cubes = GetComponentsInChildren<Animator>();
		floors = new FloorController[cubes.Length];
		for (int i = 0; i < cubes.Length; i++)
		{
			floors[i] = cubes[i].GetComponentInChildren<FloorController>();
		}
		if (active)
		{
			anim.SetBool("Active", true);
			for (int i = 0; i < cubes.Length; i++)
			{
			cubes[i].SetBool("Active", true);
			}
		} else
		{
			anim.SetBool("Active", false);
			for (int i = 0; i < cubes.Length; i++)
			{
			cubes[i].SetBool("Active", false);
			}
		}
		UpdateBlocks();
	}

	public void UpdateAnimation ()
	{
		active = !active;
		anim.SetBool("Active", active);
		for (int i = 0; i < cubes.Length; i++)
		{
			cubes[i].SetBool("Active", active);
		}
		UpdateBlocks();
	}

	void UpdateBlocks ()
	{
		if (active)
		{
			for (int i = 0; i < cubes.Length; i++)
			{
				floors[i].bloqued = false;
			}
		} else 
		{
			for (int i = 0; i < cubes.Length; i++)
			{
				floors[i].bloqued = true;
			}	
		}
	}
}
