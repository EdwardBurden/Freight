using ActionLists;
using System.Linq;
using UnityEngine;

//TODO make this into a circle location action, which will be stopped when WaitForPlatformToBeFreeAction decides to
public class CirclePositionAction : Action
{
	private const int WAYPOINT_AMOUNT = 10;
	//movemnt
	private Vector3 startPostion;
	private Vector3 targetPosition;
	private float distance;
	private float movementUpdate = 0;
	private int wayPointIndex;
	private Quaternion targetRotation;
	private float rotationUpdate = 0;
	private float turnDistance;
	private Quaternion startRotation;

	//variables
	private Vector3 position;
	private float radius;
	private Vector3[] waypoints;
	private Ship shipObject;

	public CirclePositionAction(Vector3 position, Ship shipObject, float radius) : base(false)
	{
		this.shipObject = shipObject;
		this.position = position;
		this.radius = radius;
		movementUpdate = 0;
		waypoints = CreateWaypoints(position, radius, WAYPOINT_AMOUNT);
	}

	private Vector3[] CreateWaypoints(Vector3 postion, float radius, int waypointAmount)
	{
		Vector3[] waypoints = new Vector3[waypointAmount];
		float angleBetween = 360.0f / waypointAmount;
		float totalAngle = Random.Range(0,360);
		for (int i = 0; i < waypointAmount; i++)
		{
			Vector3 dir = new Vector3(Mathf.Sin(totalAngle), 0, Mathf.Cos(totalAngle));
			waypoints[i] = postion + dir * radius;
			totalAngle += angleBetween;
		}
		return waypoints;
	}


	public override void OnInitialise()
	{	
		base.OnInitialise();
		wayPointIndex = 0;
		targetPosition = waypoints[wayPointIndex];
		startPostion = shipObject.transform.position;
		distance = Vector3.Distance(startPostion, targetPosition);
		movementUpdate = 0;
		startRotation= shipObject.transform.rotation;
		targetRotation = Quaternion.LookRotation(waypoints[wayPointIndex] - startPostion);
	}

	public override void OnUpdate()
	{
		float stepSize = (Time.deltaTime * shipObject.GetSpeed()) / distance;
		float rotationStepSize = (Time.deltaTime * shipObject.GetTurnSpeed()) / turnDistance;
		movementUpdate += stepSize;
		rotationUpdate += rotationStepSize;
		if (movementUpdate >= 1)
		{
			SetNextWaypointTarget();
			return;
		}
		shipObject.transform.position = Vector3.Lerp(startPostion, targetPosition, movementUpdate);
		shipObject.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, rotationUpdate);
	}

	private void SetNextWaypointTarget()
	{
		
		startPostion = targetPosition;
		if (wayPointIndex == waypoints.Length - 1)
		{
			wayPointIndex = 0;
		}
		else
		{
			wayPointIndex++;
		}
		startRotation = shipObject.transform.rotation;
		targetRotation = Quaternion.LookRotation(waypoints[wayPointIndex] - startPostion);
		targetPosition = waypoints[wayPointIndex];
		distance = Vector3.Distance(startPostion, targetPosition);
		turnDistance = Quaternion.Angle(startRotation, targetRotation);
		movementUpdate = 0;
		rotationUpdate = 0;
	}


}