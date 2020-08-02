using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

	public bool fast = false;
	public bool activeByTurn = true;
	public int indexState = 0;

	protected FloorController[] floor;
	protected Animator anim;

	protected string[] animations = new string[3] {"Off", "Medium", "On"}; 

	void Awake ()
	{	
		floor = new FloorController[1];
		anim = GetComponent<Animator>();
		anim.SetTrigger(animations[indexState]);
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.up, out hit, 1f))
		{
			floor[0] = hit.collider.gameObject.GetComponentInParent<FloorController>();
		}
		UpdateBlocks();
		if (!activeByTurn) floor[0].afiliedObject = gameObject;
	}

	public void StartTurnDamage ()
	{	
		if (activeByTurn) ChangeState();
	}

	public void Colides()
	{
		CancelInvoke();
		Invoke("ChangeState", 0.01f);
	}

	public void ChangeState ()
	{
		indexState++;
		if (indexState > 2) 
		{
			if (fast) {
				indexState = 1;
				anim.SetTrigger("BackPos");
			}
			else 
			{
				indexState = 0;
				anim.SetTrigger("NextPos");
			}
		}
		else anim.SetTrigger("NextPos");
		UpdateBlocks();
	}

	protected virtual void UpdateBlocks ()
	{
		if (indexState == 2)
		{
			for (int i = 0; i < floor.Length; i++)
			{
				floor[i].stopHere = true;
				floor[i].tipe = "Damage";
			}
		} else
		{
			for (int i = 0; i < floor.Length; i++)
			{
				floor[i].stopHere = false;
				floor[i].tipe = "";
			}
		}
	}
}
