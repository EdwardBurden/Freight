using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehaviour : MonoBehaviour
{
	public float turnSpeed;
	private const string PICKUP_ANIAMTION_TRIGGER = "PickUp";
	private GameObject pickup;
	private Animator animator;
	public event Action PickUpComplete;

	private const float MINROTATIONDIFFERENCE = 0.001f;

	public void StartPickup(GameObject pick)
	{
		pickup = pick;
		animator = gameObject.GetComponent<Animator>();
	}

	private void Update()
	{
		if (pickup == null)
		{
			return;
		}
		if (!IsInPlace())
		{
			RotateeToPickUp();
			return;
		}
		ReadyToPickup();
	}

	private bool IsInPlace() //could add to the movement behaviour
	{
		return Quaternion.Angle(this.transform.rotation, pickup.transform.rotation) < MINROTATIONDIFFERENCE;
	}

	private void RotateeToPickUp()
	{
		this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, pickup.transform.rotation, turnSpeed * Time.deltaTime);
	}

	private void ReadyToPickup()
	{
		animator.SetBool(PICKUP_ANIAMTION_TRIGGER, true);
	}
	public void ClampEngaged()
	{
		pickup.transform.SetParent(this.transform);
	}

	public void AniamtionComplete()
	{
		pickup = null;
		animator.SetBool(PICKUP_ANIAMTION_TRIGGER, false);
		PickUpComplete?.Invoke();
	}
}
