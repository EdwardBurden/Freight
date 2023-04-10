using ActionLists;
using UnityEngine;

//TODO make this into a circle location action, which will be stopped when WaitForPlatformToBeFreeAction decides to
public class MovenNearAction : Action
{
	private Vector3 startPostion;
	private Vector3 targetPosition;
	private Vector3 nearTargetPosition;
	private float distance;
	private float movementUpdate = 0;

	private float distanceNear;

	private Ship shipObject;

	public MovenNearAction(Vector3 position, Ship shipObject, float distanceNear, bool isBlocking) : base(isBlocking)
	{
		this.shipObject = shipObject;
		this.targetPosition = position;
		this.distanceNear = distanceNear;
		movementUpdate = 0;
	}

	public override void OnInitialise()
	{
		Vector3 randomDirection = new Vector3(Random.Range(-distanceNear, distanceNear), 0, Random.Range(-distanceNear, distanceNear));
		nearTargetPosition = targetPosition + randomDirection;

		base.OnInitialise();
		startPostion = shipObject.transform.position;
		distance = Vector3.Distance(startPostion, nearTargetPosition);
	}

	public override void OnUpdate()
	{
		float stepSize = (Time.deltaTime * shipObject.GetSpeed()) / distance;
		movementUpdate += stepSize;
		if (movementUpdate >= 1)
		{
			isFinished = true;
			return;
		}
		shipObject.transform.position = Vector3.Lerp(startPostion, nearTargetPosition, movementUpdate);
	}


}