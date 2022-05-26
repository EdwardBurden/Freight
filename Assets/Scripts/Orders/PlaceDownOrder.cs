using Behaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceDownOrder : InteractionOrder
{
	private MoveBehaviour movementBehaviour;
	private SingleCarryBehaviour carryBehaviour;

	public PlaceDownOrder(InteractableObject targetPlaceDownObject, Orderable orderable, Vector3 postion) : base(orderable, targetPlaceDownObject, postion)
	{
		carryBehaviour = orderable.GetComponent<SingleCarryBehaviour>();
		movementBehaviour = orderable.GetComponent<MoveBehaviour>();
		carryBehaviour.carryObject.RegisterCarrier(null);
	}

	public override void BeginOrder()
	{
		movementBehaviour.movementComplete += ReachedTarget;
		carryBehaviour.PlaeDownComplete += OrderComplete;

		movementBehaviour.StartMovement(targetPosition);
	}

	private void ReachedTarget()
	{
		carryBehaviour.StartPlaceDown();
	}

	public override void EndOrder()
	{
		movementBehaviour.StopMovement();
		carryBehaviour.StopPlaceDown();
		movementBehaviour.movementComplete -= ReachedTarget;
		carryBehaviour.PlaeDownComplete -= OrderComplete;
	}

	public static bool CanCreate(ControllableObject controlledObject, Vector3 clickPosition)
	{
		SingleCarryBehaviour carryBehaviour = controlledObject.gameObject.GetComponent<SingleCarryBehaviour>();
		if (carryBehaviour == null)
		{
			return false;
		}
		Freight freight = carryBehaviour.carryObject;
		if (freight == null)
		{
			return false;
		}

		return carryBehaviour.HasCargo() && freight.HasCarrier();
	}

	public override void CancelOrder()
	{
		throw new System.NotImplementedException();
	}
}

