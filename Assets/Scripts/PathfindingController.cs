using AStar;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathfindingController : MonoBehaviour
{
	private AStarGrid aStarGrid;

	[SerializeField] private Transform start;
	[SerializeField] private Transform end;

	private List<AStarNode> path = new List<AStarNode>(); //temp

	private void Awake()
	{
		aStarGrid = GetComponent<AStarGrid>();
	}

	private void Start()
	{
		aStarGrid.CreateGrid();
		FindPath(start.position, end.position);
	}

	public bool FindPath(Vector3 worldStartPos, Vector3 worldEndPos)
	{
		AStarNode start = aStarGrid.FindNode(worldStartPos);
		AStarNode end = aStarGrid.FindNode(worldEndPos);
		path = AStarAlgorithm.FindPath(aStarGrid, start, end);
		return false;
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
