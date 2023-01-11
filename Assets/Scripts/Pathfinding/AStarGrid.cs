using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

namespace AStar
{

	public class AStarGrid : MonoBehaviour
	{
		[SerializeReference] public Transform gridPosTransform;
		[SerializeReference] public LayerMask unnavigableLayer;
		[SerializeReference] public float nodeRadius;
		[SerializeReference] public Vector3 worldGridSize;

		private AStarNode[,,] grid;
		private float nodeDiameter;
		private Vector3Int gridSize;

		public int maxSize => gridSize.x * gridSize.y * gridSize.z;

		private void Awake()
		{
			nodeDiameter = nodeRadius * 2;
			gridSize = Vector3Int.RoundToInt(worldGridSize / nodeDiameter);
		}

		public void CreateGrid()
		{
			grid = new AStarNode[gridSize.x, gridSize.y, gridSize.z];
			Vector3 start = gridPosTransform.position - (worldGridSize / 2);


			for (int x = 0; x < gridSize.x; x++)
			{
				for (int y = 0; y < gridSize.y; y++)
				{
					for (int z = 0; z < gridSize.z; z++)
					{
						Vector3 worldPoint = start + (new Vector3(x, y, z) * nodeDiameter) + (nodeRadius * Vector3.one);
						bool navigable = !Physics.CheckSphere(worldPoint, nodeRadius, unnavigableLayer);

						grid[x, y, z] = new AStarNode(navigable, worldPoint, new Vector3Int(x, y, z));
					}
				}
			}
		}

		public AStarNode FindNode(Vector3 worldPostion)
		{
			worldPostion -= (gridPosTransform.position - (worldGridSize / 2));
			float x = Mathf.Clamp01(worldPostion.x / worldGridSize.x);
			float y = Mathf.Clamp01(worldPostion.y / worldGridSize.y);
			float z = Mathf.Clamp01(worldPostion.z / worldGridSize.z);

			int xGrid = Mathf.FloorToInt(Mathf.Clamp((gridSize.x) * x, 0, gridSize.x - 1));
			int yGrid = Mathf.FloorToInt(Mathf.Clamp((gridSize.y) * y, 0, gridSize.y - 1));
			int zGrid = Mathf.FloorToInt(Mathf.Clamp((gridSize.z) * z, 0, gridSize.z - 1));
			/*int xGrid = Mathf.RoundToInt((gridSize.x - 1) * x);
			int yGrid = Mathf.RoundToInt((gridSize.y - 1) * y);
			int zGrid = Mathf.RoundToInt((gridSize.z - 1) * z);*/
			return grid[xGrid, yGrid, zGrid];
		}

		public List<AStarNode> GetSurroundingNodes(AStarNode aStarNode)
		{
			List<AStarNode> surroundingNodes = new List<AStarNode>();
			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					for (int z = -1; z <= 1; z++)
					{
						if (x == 0 && y == 0 && z == 0)
						{
							continue;
						}
						Vector3Int pos = aStarNode.gridPosition + new Vector3Int(x, y, z);
						if (pos.x >= 0 && pos.x < gridSize.x && pos.y >= 0 && pos.y < gridSize.y && pos.z >= 0 && pos.z < gridSize.z)
						{
							surroundingNodes.Add(grid[pos.x, pos.y, pos.z]);
						}
					}
				}
			}
			return surroundingNodes;

		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.grey;
			Gizmos.DrawWireCube(gridPosTransform.position, worldGridSize);

			if (grid == null)
			{
				return;
			}
			/*foreach (AStarNode aStarNode in grid)
			{
				if (!aStarNode.navigable)
				{
					Gizmos.DrawCube(aStarNode.worldPostion, Vector3.one);
				}
			}*/

			Gizmos.color = Color.red;
		}
	}
}
