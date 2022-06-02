using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarryStorage))]
public class CollectBehaviour : CustomBehaviour
{
	private const float MINROTATIONDIFFERENCE = 0.001f;
	private const string PICKUP_ANIMATION_TRIGGER = "PickUp";

	public event Action PickUpComplete;

	public float turnSpeed;
	public Freight target;

	public override void StopBehaviour()
	{
		target = null;
		StopCoroutine(PickUp());
	}



	private IEnumerator PickUp()
	{
		if (target == null)
		{
			yield return null;
		}
		while (!IsMatchingRotation(target.transform.rotation, MINROTATIONDIFFERENCE))
		{
			RotateToRotation(target.transform.rotation, turnSpeed);
			yield return null;
		}
		ReadyToPickup();
	}

	private void ReadyToPickup()
	{
		animator.SetBool(PICKUP_ANIMATION_TRIGGER, true);
	}

	public void StartPickup(Freight pick)
	{
		target = pick;
		StartCoroutine(PickUp());

	}
	public void ClampEngaged() //could be handled in order(is logic)
	{
		if (target != null)
		{
			target.transform.SetParent(GetComponent<CarryStorage>().GetAvailableStorageTransform());
			target.Carry();
		}
	}

	public void PickUpAnimationComplete()
	{
		animator.SetBool(PICKUP_ANIMATION_TRIGGER, false);
		target = null;
		PickUpComplete?.Invoke();
	}
}
