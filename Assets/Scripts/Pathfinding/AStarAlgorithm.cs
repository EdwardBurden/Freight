using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace AStar
{
	public static class AStarAlgorithm
	{
		public static List<AStarNode> FindPath(AStarGrid grid, AStarNode startNode, AStarNode targetNode)
		{
			//sanitise the grid first 
			List<AStarNode> openSet = new List<AStarNode>();
			HashSet<AStarNode> closedSet = new HashSet<AStarNode>();
			openSet.Add(startNode);

			while (openSet.Count > 0)
			{
				AStarNode node = openSet[0];
				for (int i = 1; i < openSet.Count; i++)
				{
					if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
					{
						if (openSet[i].hCost < node.hCost)
							node = openSet[i];
					}
				}

				openSet.Remove(node);
				closedSet.Add(node);

				if (node == targetNode)
				{
					return ConvertNodeChainToList(startNode, targetNode);
				}

				foreach (AStarNode neighbour in grid.GetSurroundingNodes(node))
				{
					if (!neighbour.navigable || closedSet.Contains(neighbour))
					{
						continue;
					}

					float newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
					if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
					{
						neighbour.gCost = newCostToNeighbour;
						neighbour.hCost = GetDistance(neighbour, targetNode);
						neighbour.parent = node;

						if (!openSet.Contains(neighbour))
							openSet.Add(neighbour);
					}
				}
			}
			return new List<AStarNode>();
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
			float dist = Vector3.Distance(start.worldPostion, end.worldPostion);
			return dist;
		}
	}


}
