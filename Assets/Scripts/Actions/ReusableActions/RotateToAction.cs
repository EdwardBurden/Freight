using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToAction : ShipAction
{
	Quaternion rotation;
	public RotateToAction(Quaternion rotation, ShipObject shipObject, bool isBlocking = false) : base(shipObject, isBlocking)
	{
		this.rotation = rotation;
	}

	public override void OnUpdate()
	{
		if (!IsAtRotation())
		{
			RotateTo();
		}
		else
		{
			isFinished = true;
		}
	}

	private bool IsAtRotation()
	{
		return Mathf.Approximately(Mathf.Abs(Quaternion.Dot(shipObject.transform.rotation, rotation)), 1.0f);

	}


	private void RotateTo()
	{
		shipObject.transform.rotation = Quaternion.RotateTowards(shipObject.transform.rotation, rotation, shipObject.turnSpeed * Time.deltaTime);

	}
}
