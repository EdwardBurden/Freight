using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace AStar
{
	public static class AStarAlgorithm
	{
		public static List<AStarNode> FindPath(AStarGrid grid, AStarNode startNode, AStarNode targetNode)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			//sanitise the grid first 
			AStarHeap<AStarNode> openSet = new AStarHeap<AStarNode>(grid.maxSize);
			HashSet<AStarNode> closedSet = new HashSet<AStarNode>();
			openSet.AddItem(startNode);

			while (openSet.Count() > 0)
			{
				AStarNode node = openSet.RemoveFirst();
				closedSet.Add(node);

				if (node == targetNode)
				{
					stopwatch.Stop();
					UnityEngine.Debug.Log(stopwatch.ElapsedMilliseconds);
					return ConvertNodeChainToList(startNode, targetNode);
				}

				foreach (AStarNode neighbour in grid.GetSurroundingNodes(node))
				{
					if (!neighbour.navigable || closedSet.Contains(neighbour))
					{
						continue;
					}

					float newCostToNeighbour = node.gCost + GetDistance(node, neighbour) + neighbour.modifiedMovementPenalty;
					if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
					{
						neighbour.gCost = newCostToNeighbour;
						neighbour.hCost = GetDistance(neighbour, targetNode);
						neighbour.parent = node;

						if (!openSet.Contains(neighbour))
						{
							openSet.AddItem(neighbour);
						}
						else
						{
							openSet.UpdateItem(neighbour);
						}
					}
				}
			}
			return new List<AStarNode>();
		}

		public static Vector3[] SimplifyPath(List<AStarNode> aStarNodes , Vector3 end)
		{
			if (aStarNodes.Count == 0)
			{
				return null;
			}

			List<Vector3> waypoints = new List<Vector3>();
			Vector3 marchingDirection = Vector3.zero;

			for (int i = 0; i < aStarNodes.Count - 1; i++)
			{
				AStarNode start = aStarNodes[i];
				Vector3 newDirection = aStarNodes[i + 1].worldPostion - start.worldPostion;
				if (newDirection != marchingDirection)
				{
					marchingDirection = newDirection;
					waypoints.Add(start.worldPostion);
				}
			}
			//waypoints.Add(aStarNodes[aStarNodes.Count - 1].worldPostion);
			waypoints.Add(end);
			return waypoints.ToArray();
		}

		private static List<AStarNode> ConvertNodeChainToList(AStarNode start, AStarNode end)
		{
			List<AStarNode> path = new List<AStarNode>();
			AStarNode current = end;

			while (current != start)
			{
				path.Add(current);
				current = current.parent;
			}
			path.Reverse();
			return path;
		}


		private static float GetDistance(AStarNode start, AStarNode end)
		{
			float dist = Vector3.SqrMagnitude(start.worldPostion - end.worldPostion);
			return dist;
		}
	}


}
