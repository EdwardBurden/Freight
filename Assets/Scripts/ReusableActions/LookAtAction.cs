using ActionLists;
using UnityEngine;

public class LookAtAction : Action
{
	private Ship shipObject;

	private Vector3 target;

	public LookAtAction(Vector3 position, Ship shipObject, bool isBlocking = false) : base( isBlocking)
	{
		this.shipObject = shipObject;
		this.target = position;
	}

	public override void OnUpdate()
	{
		if (!shipObject.gameObject.IsLookingAtTarget(target))
		{
			LookAt();
		}
		else
		{
			isFinished = true;
		}
	}

	private void LookAt()
	{
		Quaternion look = Quaternion.LookRotation(shipObject.gameObject.GetDirection(target), Vector3.up);
		shipObject.transform.rotation = Quaternion.RotateTowards(shipObject.transform.rotation, look, shipObject.turnSpeed * Time.deltaTime);
	}
}
