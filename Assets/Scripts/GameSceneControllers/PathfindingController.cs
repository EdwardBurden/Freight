using AStar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PathfindingController : Singleton<PathfindingController>, IGameController
{

	[SerializeField] private int capsuleCheckRadius = 3;
	private AStarGrid aStarGrid;
	private Queue<PathRequest> pathRequests = new Queue<PathRequest>();
	private bool isProcessingPath; //blocks atm

	private void Awake()
	{
		_instance = this;
		aStarGrid = GetComponent<AStarGrid>();
	}

	public void Init()
	{
		aStarGrid.CreateGrid();
	}

	public void Requestpath(Vector3 startPos, Vector3 endPos, Action<Vector3[], bool> callback)
	{
		pathRequests.Enqueue(new PathRequest(startPos, endPos, callback));
		TryProcess();
	}

	public void TryProcess()
	{
		if (!isProcessingPath && pathRequests.Count > 0)
		{
			PathRequest pathRequest = pathRequests.Dequeue();
			ProcessPath(pathRequest);
		}
	}

	private void ProcessPath(PathRequest pathRequest)
	{
		isProcessingPath = true;
		pathRequest.process = StartCoroutine(FindPath(pathRequest));
	}

	private void ProccessingFinished(PathRequest pathRequest, Vector3[] path, bool success)
	{
		pathRequest.callback?.Invoke(path, success);
		isProcessingPath = false;
		TryProcess();
	}

	private IEnumerator FindPath(PathRequest pathRequest)
	{
		Vector3[] waypoints = null;
		bool success = false;
		if (!Physics.CheckCapsule(pathRequest.pathStart, pathRequest.pathEnd, capsuleCheckRadius, aStarGrid.unnavigableLayer))
		{
			success = true;
			waypoints = new Vector3[] { pathRequest.pathStart, pathRequest.pathEnd };
		}
		else
		{
			AStarNode start = aStarGrid.FindNode(pathRequest.pathStart);
			AStarNode end = aStarGrid.FindNode(pathRequest.pathEnd);

			var pathTask = Task.Run(() => AStarAlgorithm.FindPath(aStarGrid, start, end));
			while (!pathTask.IsCompleted)
			{
				yield return null;
			}
			success = pathTask.Result.Count > 0;
			waypoints = AStarAlgorithm.SimplifyPath(pathTask.Result, pathRequest.pathEnd);
		}
		ProccessingFinished(pathRequest, waypoints, success);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		if (aStarGrid == null)
		{
			return;
		}
	}

	internal void UpdatedGrid(Vector3 position, int space)
	{
		aStarGrid.UpdateGrid(position, space); //TODO
	}
}


struct PathRequest //todo move
{
	public Vector3 pathStart;
	public Vector3 pathEnd;
	public Action<Vector3[], bool> callback;

	public Coroutine process;

	public PathRequest(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
	{
		this.pathStart = pathStart;
		this.pathEnd = pathEnd;
		this.callback = callback;
		process = null;
	}


}