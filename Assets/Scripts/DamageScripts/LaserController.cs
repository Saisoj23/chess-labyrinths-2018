using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : DamageController {

	public GameObject startPosition;
	public GameObject finalPosition;
	
	int defaultLayer = (1 << 0);
	LineRenderer line;
	BoxCollider box;
	Ray ray = new Ray();
	RaycastHit hit;

	void Awake ()
	{
		line = GetComponent<LineRenderer>();
		line.SetPosition(0, startPosition.transform.position);
		ray.direction = -transform.up;
		ray.origin = startPosition.transform.position;
		//Debug.DrawRay(ray.origin, ray.direction, Color.red, Mathf.Infinity);
		UpdateFinalPos();
		anim = GetComponent<Animator>();
		anim.SetTrigger(animations[indexState]);
		UpdateBlocks();
	}

	public void UpdateFinalPos()
	{
		if (Physics.Raycast(ray, out hit, 100f, defaultLayer))
		{
			finalPosition.transform.position = hit.point;
			line.SetPosition(1, hit.point);
		}
		RaycastHit[] hits = Physics.RaycastAll(ray, Vector3.Distance(ray.origin, hit.point), LevelController.rayBoxLayer);
		floor = new FloorController[hits.Length];
		for (int i = 0; i < hits.Length; i++)
		{
			floor[i] = hits[i].collider.gameObject.GetComponentInParent<FloorController>();
			if (!activeByTurn) floor[i].afiliedObject = gameObject;
		}
	}
}
