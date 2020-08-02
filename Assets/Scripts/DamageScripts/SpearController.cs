using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearController : DamageController {

	protected override void UpdateBlocks()
	{
		if (indexState == 2)
		{
			for (int i = 0; i < floor.Length; i++)
			{
				floor[i].stopHere = true;
				floor[i].tipe = "Damage";
				floor[i].bloqued = true;
			}
		} else
		{
			for (int i = 0; i < floor.Length; i++)
			{
				floor[i].stopHere = false;
				floor[i].tipe = "";
				floor[i].bloqued = false;
			}
		}
	}
}
