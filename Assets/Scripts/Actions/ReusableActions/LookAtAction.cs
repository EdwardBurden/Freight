using ActionLists;
using UnityEngine;

public class LookAtAction : ShipObjectAction
{
	private Vector3 target;

	public LookAtAction(Vector3 position, ShipObject shipObject, bool isBlocking = false) : base(shipObject, isBlocking)
	{
		this.target = position;
	}

	public override void OnUpdate()
	{
		if (!IsLookingAtTarget(target))
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
		Quaternion look = Quaternion.LookRotation(GetDirection(target), Vector3.up);
		shipObject.transform.rotation = Quaternion.RotateTowards(shipObject.transform.rotation, look, shipObject.turnSpeed * Time.deltaTime);
	}
}
