using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarryStorage))]
public class DeliverBehaviour : CustomBehaviour
{
	private const float MINROTATIONDIFFERENCE = 0.001f;
	private const string PLACEDOWN_ANIMATION_TRIGGER = "PlaceDown";

	public event Action PlaceDownComplete;

	public float turnSpeed;
	public Transform placeDownTarget;
	public Freight target;

	public override void StopBehaviour()
	{
		target = null;
		StopCoroutine(PlaceDown());
	}
	private IEnumerator PlaceDown()
	{
		if (target == null)
		{
			yield return null;
		}
		while (!IsMatchingRotation(placeDownTarget.rotation, MINROTATIONDIFFERENCE))
		{
			RotateToRotation(placeDownTarget.rotation, turnSpeed);
			yield return null;
		}
		ReadyToPlace();
	}


	public void StartPlaceDown(Transform targetInteractionTransform, Freight freight)
	{
		this.placeDownTarget = targetInteractionTransform;
		target = freight;
		StartCoroutine(PlaceDown());
	}
	private void ReadyToPlace()
	{
		animator.SetBool(PLACEDOWN_ANIMATION_TRIGGER, true);
	}

	public void ClampDisengaged()
	{
		target.transform.parent = null;
		target.Drop();
	}

	public void PlaceDownAnimationComplete()
	{
		animator.SetBool(PLACEDOWN_ANIMATION_TRIGGER, false);
		PlaceDownComplete?.Invoke();
	}
}
