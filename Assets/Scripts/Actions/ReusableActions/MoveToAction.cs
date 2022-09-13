using ActionLists;
using UnityEngine;

public class MoveToAction : Action
{
	private Ship shipObject;
	private Vector3 position;

	public MoveToAction(Vector3 position, Ship shipObject, bool isBlocking) : base( isBlocking)
	{
		this.shipObject = shipObject;
		this.position = position;
	}

	public override void OnUpdate()
	{
		if (!IsAtLocation())
		{
			MoveTo();
		}
		else
		{
			isFinished = true;
		}
	}

	private bool IsAtLocation()
	{
		return Vector3.Distance(shipObject.transform.position, position) < 0.01f;
	}

	private void MoveTo()
	{
		Vector3 direction = position - shipObject.transform.position;
		direction.Normalize();
		shipObject.transform.position += direction * Time.deltaTime * shipObject.speed;
	}

}
