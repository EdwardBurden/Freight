using ActionLists;
using UnityEngine;

public class MoveToAction : Action
{
	private Vector3 startPostion;
	private Vector3 targetPosition;
	private float distance;
	private float movementUpdate = 0;

	private Ship shipObject;

	public MoveToAction(Vector3 position, Ship shipObject, bool isBlocking) : base(isBlocking)
	{
		this.shipObject = shipObject;
		this.targetPosition = position;
		movementUpdate = 0;
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		startPostion = shipObject.transform.position;
		distance = Vector3.Distance(startPostion, targetPosition);
	}

	public override void OnUpdate()
	{
		float stepSize = (Time.deltaTime * shipObject.speed)/ distance;
		movementUpdate += stepSize;
		if (movementUpdate >= 1)
		{
			isFinished = true;
			return;
		}
		shipObject.transform.position = Vector3.Lerp(startPostion, targetPosition, movementUpdate);
	}


}
