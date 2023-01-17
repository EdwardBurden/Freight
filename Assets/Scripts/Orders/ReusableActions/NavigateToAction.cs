using ActionLists;
using AStar;
using UnityEngine;


public class NavigateToAction : Action
{
	private Vector3 targetPosition;
	private Ship shipObject;

	private Vector3 startPostion;
	private Quaternion startRotation;
	private float distance;
	private float turnDistance;
	private Quaternion targetRotation;
	private float rotationUpdate = 0;
	private float movementUpdate = 0;

	private Vector3[] wayPoints;
	private int waypointIndex;

	public NavigateToAction(Vector3 position, Ship shipObject, bool isBlocking) : base(isBlocking)
	{
		this.shipObject = shipObject;
		this.targetPosition = position;
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		startPostion = shipObject.transform.position;
		PathfindingController.instance.Requestpath(startPostion, targetPosition, OnPathfindingFinished);
	}

	private void OnPathfindingFinished(Vector3[] path, bool success)
	{
		if (success)
		{
			waypointIndex = 0;
			wayPoints = path;
			TargetNextWaypoint();
		}
		else
		{
			Debug.LogError("BIG PROBLEM");
		}
	}

	public override void OnUpdate()
	{
		if (wayPoints == null || wayPoints.Length == 0)
		{
			//not finished because not started?
			return;
		}
		if (movementUpdate >= 1)
		{
			waypointIndex++;
			if (waypointIndex >= wayPoints.Length)
			{
				isFinished = true;
				return;
			}
			else
			{
				TargetNextWaypoint();
			}
		}

		UpdatePosition();
	}

	private void TargetNextWaypoint()
	{
		startPostion = shipObject.transform.position;
		startRotation = shipObject.transform.rotation;
		targetRotation = Quaternion.LookRotation(wayPoints[waypointIndex] - startPostion);
		distance = Vector3.Distance(startPostion, wayPoints[waypointIndex]);
		turnDistance = Quaternion.Angle(startRotation, targetRotation);
		movementUpdate = 0;
		rotationUpdate = 0;
	}

	private void UpdatePosition()
	{
		float stepSize = (Time.deltaTime * shipObject.GetSpeed()) / distance;
		float rotationStepSize = (Time.deltaTime * shipObject.GetTurnSpeed()) / turnDistance;
		movementUpdate += stepSize;
		rotationUpdate += rotationStepSize;
		shipObject.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, rotationUpdate);
		shipObject.transform.position = Vector3.Lerp(startPostion, wayPoints[waypointIndex], movementUpdate);
	}
}
