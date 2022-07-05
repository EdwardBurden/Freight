using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToAction : GameObjectAction
{
	Quaternion rotation;
	float turnSpeed;
	public RotateToAction(Quaternion rotation, float speed, GameObject gameObject, bool isBlocking = false) : base(gameObject, isBlocking)
	{
		this.rotation = rotation;
		turnSpeed = speed;
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
		return Mathf.Approximately(Mathf.Abs(Quaternion.Dot(gameObject.transform.rotation, rotation)), 1.0f);

	}


	private void RotateTo()
	{
		gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, rotation, turnSpeed * Time.deltaTime);

	}
}
