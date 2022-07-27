using ActionLists;
using UnityEngine;

public class MoveToAction : Action
{
	private ShipObject shipObject;
	private Vector3 position;

	public MoveToAction(Vector3 position, ShipObject shipObject, bool isBlocking) : base( isBlocking)
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
		return Vector3.Distance(shipObject.transform.position, position) < 0.1f;
	}

	private void MoveTo()
	{
		Vector3 direction = position - shipObject.transform.position;
		direction.Normalize();
		shipObject.transform.position += direction * Time.deltaTime * shipObject.speed;
	}

}
