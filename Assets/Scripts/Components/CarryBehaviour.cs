using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviours
{
	public class CarryBehaviour : CustomBehaviour
	{
		public float turnSpeed;
		private const string PICKUP_ANIAMTION_TRIGGER = "PickUp";
		private Carryable pickup;
		private Animator animator;
		public event Action PickUpComplete;

		private const float MINROTATIONDIFFERENCE = 0.001f;

		private void Awake()
		{
			animator = gameObject.GetComponent<Animator>();
		}

		public void StartPickup(Carryable pick)
		{
			pickup = pick;
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

		public void StopPickUp()
		{
			pickup = null;
			animator.SetBool(PICKUP_ANIAMTION_TRIGGER, false);
		}

		private void ReadyToPickup()
		{
			animator.SetBool(PICKUP_ANIAMTION_TRIGGER, true);
		}

		public bool HasCarryable()
		{
			return GetComponentInChildren<Carryable>() != null;
		}

		public void ClampEngaged()
		{
			pickup.transform.SetParent(this.transform);
			pickup.Carry();
		}

		public void AniamtionComplete()
		{
			StopPickUp();
			PickUpComplete?.Invoke();
		}
	}
}