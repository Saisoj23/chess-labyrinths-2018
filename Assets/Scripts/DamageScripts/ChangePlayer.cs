using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour {
	public GameObject player;
	
	void Awake ()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.up, out hit, 1, LevelController.rayBoxLayer))
		{
			FloorController floor = hit.collider.gameObject.GetComponent<FloorController>();
			floor.afiliedObject = gameObject;
			floor.tipe = "Change";
			floor.stopHere = true;
		}
	}
}
