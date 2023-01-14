using ActionLists;
using AStar;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;


public class NavigateToAction : Action
{
	private Vector3 startPostion;
	private Vector3 targetPosition;

	private Ship shipObject;
	private AStarUnit aStarUnit;

	public NavigateToAction(Vector3 position, Ship shipObject, bool isBlocking) : base(isBlocking)
	{
		this.shipObject = shipObject;
		this.targetPosition = position;
		aStarUnit = shipObject.GetComponent<AStarUnit>();
	}

	public override void OnInitialise()
	{
		base.OnInitialise();
		startPostion = shipObject.transform.position;
		aStarUnit.CompletedPathMovementEvent += OnPathCompleted;
		aStarUnit.MoveOnPath(PathfindingController.instance.FindPath(startPostion, targetPosition));
	}

	private void OnPathCompleted()
	{
		isFinished = true;
		aStarUnit.CompletedPathMovementEvent -= OnPathCompleted;
	}

	public override void Cancel()
	{
		base.Cancel();
		aStarUnit.CompletedPathMovementEvent -= OnPathCompleted;
	}

	public override void OnUpdate()
	{
		//DO NOTHING
	}
}
