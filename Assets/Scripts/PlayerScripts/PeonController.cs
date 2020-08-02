using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeonController : PlayerController {

	public override void StartPlayerTurn () 
	{
		Ray ray = new Ray();
		RaycastHit hit;
		for (int i = 0; i < direccions.Length; i++)
		{	
			ray.origin = finalPosition + (Vector3.up * 0.25f);
			ray.direction = direccions[i];
			FloorController final = Raycasting(ray, out hit);
			//Debug.DrawLine(ray.origin, hit.point, colors[i], 2f);
			if (final != null) 
			{
				final.Active(true);
			}
		}
	}
}
