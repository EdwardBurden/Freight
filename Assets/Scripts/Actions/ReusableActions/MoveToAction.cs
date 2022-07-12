using ActionLists;
using UnityEngine;

public class MoveToAction : ShipAction
{
	Vector3 position;

	public MoveToAction(Vector3 position, ShipObject shipObject, bool isBlocking) : base(shipObject, isBlocking)
	{
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
