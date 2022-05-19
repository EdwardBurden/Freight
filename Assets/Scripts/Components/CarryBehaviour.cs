using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviours
{
	public class CarryBehaviour : CustomBehaviour
	{
		public float turnSpeed; //or goto ship behaviour, get speed for defintion 
		private const string PICKUP_ANIMATION_TRIGGER = "PickUp";
		private const string PLACEDOWN_ANIMATION_TRIGGER = "PlaceDown";
		public Transform[] carryPositions;
		private Animator animator;
		public event Action PickUpComplete;
		public event Action PlaeDownComplete;

		private const float MINROTATIONDIFFERENCE = 0.001f;

		private GameObject targetObjectParent;
		private Carryable targetObject;

		private void Awake()
		{
			animator = gameObject.GetComponent<Animator>();
		}

		public void StartPickup(Carryable pick)
		{
			targetObject = pick;
			StartCoroutine(PickUp());
		}

		public void StartPlaceDown(GameObject targetParent)
		{
			targetObject = GetComponentInChildren<Carryable>();
			targetObjectParent = targetParent;
			StartCoroutine(PlaceDown());
		}
		public void StopPickUp()
		{
			targetObject = null;
			animator.SetBool(PICKUP_ANIMATION_TRIGGER, false);
			StopAllCoroutines();
		}

		public void StopPlaceDown()
		{
			animator.SetBool(PLACEDOWN_ANIMATION_TRIGGER, false);
			StopAllCoroutines();
		}

		private IEnumerator PickUp()
		{
			if (targetObject == null)
			{
				yield return null;
			}
			while (!IsMatchingRotation(targetObject.transform.rotation))
			{
				RotateToRotation(targetObject.transform.rotation);
				yield return null;
			}
			ReadyToPickup();
		}

		private IEnumerator PlaceDown()
		{
			if (targetObject == null)
			{
				yield return null;
			}
			while (!IsMatchingRotation(Quaternion.Euler(Vector3.forward)))
			{
				RotateToRotation(Quaternion.Euler(Vector3.forward));
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
			targetObject.transform.SetParent(this.transform);
			targetObject.Carry();
		}

		public void PickUpAnimationComplete()
		{
			StopPickUp();
			PickUpComplete?.Invoke();
		}

		public void ClampDisengaged()
		{
			targetObject.transform.parent = targetObjectParent.transform;
			targetObject.Drop();
		}

		public void PlaceDownAnimationComplete()
		{
			StopPlaceDown();
			PlaeDownComplete?.Invoke();
		}


	}
}