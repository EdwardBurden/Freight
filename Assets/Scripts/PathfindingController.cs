using AStar;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathfindingController : Singleton<PathfindingController>
{
	private AStarGrid aStarGrid;

	[SerializeField] private Transform start;
	[SerializeField] private Transform end;

	private List<AStarNode> path = new List<AStarNode>(); //temp

	private void Awake()
	{
		_instance = this;
		aStarGrid = GetComponent<AStarGrid>();
	}

	private void Start()
	{
		aStarGrid.CreateGrid();
		FindPath(start.position, end.position);
	}

	private void Update()
	{
		FindPath(start.position, end.position);
	}

	public List<AStarNode> FindPath(Vector3 worldStartPos, Vector3 worldEndPos)
	{
		AStarNode start = aStarGrid.FindNode(worldStartPos);
		AStarNode end = aStarGrid.FindNode(worldEndPos);

		//checl they are naviable, if not find nearest till you can connect them

		path = AStarAlgorithm.FindPath(aStarGrid, start, end);
		return path;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		if (aStarGrid == null)
		{
			return;
		}
		foreach (AStarNode aStarNode in path)
		{
			Gizmos.DrawCube(aStarNode.worldPostion, Vector3.one);
		}
		Gizmos.DrawCube(aStarGrid.FindNode(start.position).worldPostion, Vector3.one);
		Gizmos.DrawCube(aStarGrid.FindNode(end.position).worldPostion, Vector3.one);
	}
}
