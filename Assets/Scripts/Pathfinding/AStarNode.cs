using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
	public class AStarNode : IHeapItem<AStarNode>
	{
		public bool navigable;
		public Vector3 worldPostion;
		public Vector3Int gridPosition;
		public float movementPenalty;

		public float gCost;
		public float hCost;
		public float fCost => gCost + hCost;

		private int heapIndex;

		public int HeapIndex { get => heapIndex; set => heapIndex = value; }

		public AStarNode parent;

		public AStarNode(bool navigable, Vector3 worldPostion, Vector3Int gridPosition, float movementPenalty)
		{
			this.navigable = navigable;
			this.worldPostion = worldPostion;
			this.gridPosition = gridPosition;
			this.movementPenalty = movementPenalty;
		}

		public int CompareTo(AStarNode other)
		{
			int compare = fCost.CompareTo(other.fCost);
			if (compare == 0)
			{
				compare = hCost.CompareTo(other.hCost);
			}
			return -compare;
		}
	}
}