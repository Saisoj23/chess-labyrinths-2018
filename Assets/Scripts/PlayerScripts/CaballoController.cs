using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaballoController : PlayerController {

	Vector3[] movePositions = new Vector3[4];
	int positionIndex = 1;
	public override void StartPlayerTurn()
	{
		Ray ray = new Ray();
		ray.direction = Vector3.down;
		RaycastHit hit;
		for (int i = 0; i < direccions.Length; i++)
		{	
			ray.origin = finalPosition + direccions[i] + Vector3.up;
			FloorController final = Raycasting(ray, out hit);
			//Debug.DrawLine(ray.origin, hit.point, colors[i], 2f);
			if (final != null) 
			{	
				final.Active(true);
			}
		}
	}

	protected override void SetDireccions()
	{
		direccions = new Vector3[8] {(Vector3.forward * 2) + Vector3.right, (Vector3.forward * 2) + Vector3.left, 
		(Vector3.right * 2) + Vector3.forward, (Vector3.right * 2) + Vector3.back,
		(Vector3.back * 2) + Vector3.left, (Vector3.back * 2) + Vector3.right,
		(Vector3.left * 2) + Vector3.back, (Vector3.left * 2) + Vector3.forward};
	}

	protected override void Move()
	{
		level.DesactiveFloor();
		movePositions[0] = transform.position;
		movePositions[1] = movePositions[0] + Vector3.up;
		movePositions[2] = finalPosition + Vector3.up;
		movePositions[3] = finalPosition;
		finalPosition = movePositions[1];
		StartCoroutine(MoveCouritine());
	}  

	public override void FinalMove()
	{
		if (positionIndex < 3)
		{
			finalPosition = movePositions[++positionIndex];
			StartCoroutine(MoveCouritine());
		}
		else 
		{	
			positionIndex = 1;
			level.StartNewTurn();
		}
	}
}
