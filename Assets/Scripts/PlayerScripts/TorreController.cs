using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreController : PlayerController {

	public override void StartPlayerTurn() 
	{
		Ray ray = new Ray();
		RaycastHit hit;
		FloorController[] colides = new FloorController[direccions.Length];
		Vector3[] firstCollides = new Vector3[direccions.Length];
		for (int i = 0; i < direccions.Length; i++)
		{
			FloorController actualCollide = null;
			FloorController lastCollide = null;
			ray.origin = finalPosition + (Vector3.up * 0.25f);
			ray.direction = direccions[i];
			do
			{
				actualCollide = Raycasting(ray, out hit);
				//Debug.DrawLine(ray.origin, hit.point, Color.red, 2f);
				if (actualCollide != null)
				{
					if (firstCollides[i] == Vector3.zero) firstCollides[i] = actualCollide.transform.position;
					lastCollide = actualCollide;
					ray.origin = actualCollide.transform.position + (Vector3.up * 0.75f);
				}
			} while (actualCollide != null && !actualCollide.stopHere);
			if (lastCollide != null) 
			{	
				colides[i] = lastCollide;
			}
		}
		for (int i = 0; i < direccions.Length; i++)
		{
			if (colides[i] != null) 
			{
				colides[i].Active(true);
				colides[i].Move(firstCollides[i], false);
			}
		}
	}
}