using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviours
{
	public class SingleCarryBehaviour : CustomBehaviour
	{
		private const float MINROTATIONDIFFERENCE = 0.001f;
		private const string PICKUP_ANIMATION_TRIGGER = "PickUp";
		private const string PLACEDOWN_ANIMATION_TRIGGER = "PlaceDown";

		public event Action PickUpComplete;
		public event Action PlaeDownComplete;

		public float turnSpeed;

		private Animator animator;
		public Freight carryObject;
		public Transform targetInteractionTransform;

		private void Awake()
		{
			animator = gameObject.GetComponent<Animator>();
		}

		public void StartPickup(Freight pick)
		{
			carryObject = pick;
			StartCoroutine(PickUp());
		}

		public void StartPlaceDown(Transform targetInteractionTransform)
		{
			this.targetInteractionTransform = targetInteractionTransform;
			carryObject = GetComponentInChildren<Freight>();
			StartCoroutine(PlaceDown());
		}


		public override void StopBehaviour()
		{
			StopAllCoroutines();
		}


		public void EndPickUp()
		{
			animator.SetBool(PICKUP_ANIMATION_TRIGGER, false);
			StopAllCoroutines();
		}

		public void EndPlaceDown()
		{
			animator.SetBool(PLACEDOWN_ANIMATION_TRIGGER, false);
			StopAllCoroutines();
		}

		private IEnumerator PickUp()
		{
			if (carryObject == null)
			{
				yield return null;
			}
			while (!IsMatchingRotation(carryObject.transform.rotation))
			{
				RotateToRotation(carryObject.transform.rotation);
				yield return null;
			}
			ReadyToPickup();
		}

		public virtual bool HasCarrySpace()
		{
			return carryObject == null;
		}

		private IEnumerator PlaceDown()
		{
			if (carryObject == null)
			{
				yield return null;
			}
			while (!IsMatchingRotation(targetInteractionTransform.rotation))
			{
				RotateToRotation(targetInteractionTransform.rotation);
				yield return null;
			}
			ReadyToPlace();
		}

		private bool IsMatchingRotation(Quaternion quaternion) //can be moved to helpers
		{
			return Quaternion.Angle(this.transform.rotation, quaternion) < MINROTATIONDIFFERENCE;
		}

		private void RotateToRotation(Quaternion quaternion) //can be moved to helpers
		{
			this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, quaternion, turnSpeed * Time.deltaTime);
		}

		private void ReadyToPickup()
		{
			animator.SetBool(PICKUP_ANIMATION_TRIGGER, true);
		}

		private void ReadyToPlace()
		{
			animator.SetBool(PLACEDOWN_ANIMATION_TRIGGER, true);
		}

		public void ClampEngaged()
		{
			carryObject.transform.SetParent(this.transform);
			carryObject.Carry();
		}

		public void PickUpAnimationComplete()
		{
			EndPickUp();
			PickUpComplete?.Invoke();
		}

		public void ClampDisengaged()
		{
			carryObject.transform.parent = null;
			carryObject.Drop();
		}

		public void PlaceDownAnimationComplete()
		{
			EndPlaceDown();
			PlaeDownComplete?.Invoke();
		}


	}
}