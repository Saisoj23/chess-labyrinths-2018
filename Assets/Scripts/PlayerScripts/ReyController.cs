using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReyController : PeonController {
	protected override void SetDireccions()
	{
		direccions = new Vector3[8] {Vector3.left, Vector3.right, Vector3.forward, Vector3.back, 
		(Vector3.left + Vector3.forward).normalized, (Vector3.forward + Vector3.right).normalized, (Vector3.right + Vector3.back).normalized, (Vector3.back + Vector3.left).normalized};
	}
}