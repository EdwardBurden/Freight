using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStar
{

	public class AStarGrid : MonoBehaviour
	{
		[SerializeReference] public Transform gridPosTransform;
		[SerializeReference] public LayerMask unnavigableLayer;
		[SerializeReference] public float nodeRadius;
		[SerializeReference] public Vector3 worldGridSize;
		[SerializeReference] public float maxMovementpenalty = 100;
		[SerializeReference] public int blurAmount = 2;

		private AStarNode[,,] grid;
		private float nodeDiameter;
		private Vector3Int gridSize;
		public int maxSize => gridSize.x * gridSize.y * gridSize.z;

		private void Awake()
		{
			nodeDiameter = nodeRadius * 2;
			gridSize = Vector3Int.RoundToInt(worldGridSize / nodeDiameter);
		}

		private IEnumerator CreateGridRoutine(Vector3 start)
		{
			for (int x = 0; x < gridSize.x; x++)
			{
				for (int y = 0; y < gridSize.y; y++)
				{
					for (int z = 0; z < gridSize.z; z++)
					{
						Vector3 worldPoint = start + (new Vector3(x, y, z) * nodeDiameter) + (nodeRadius * Vector3.one);
						bool navigable = !Physics.CheckBox(worldPoint, new Vector3(nodeRadius, nodeRadius, nodeRadius), Quaternion.identity, unnavigableLayer);
						float penalty = !navigable ? maxMovementpenalty : 0;
						grid[x, y, z] = new AStarNode(navigable, worldPoint, new Vector3Int(x, y, z), penalty);
					}
				}
			}
			BlurGrid(blurAmount);
			yield return null;
		}

		public void CreateGrid()
		{
			grid = new AStarNode[gridSize.x, gridSize.y, gridSize.z];
			Vector3 start = gridPosTransform.position - (worldGridSize / 2);
			StartCoroutine(CreateGridRoutine(start));
		}

		public void UpdateGrid(Vector3 point, int space)
		{
			Vector3 start = gridPosTransform.position - (worldGridSize / 2);
			AStarNode pointNode = FindNode(point);
			int length = (space - 1) / 2; //2
			for (int x = -length; x <= length; x++)
			{
				for (int y = -length; y <= length; y++)
				{
					for (int z = -length; z <= length; z++)
					{
						Vector3 refreshPoint = pointNode.gridPosition + new Vector3(x, y, z);
						if (refreshPoint.x < 0 || refreshPoint.y < 0 || refreshPoint.z < 0 || refreshPoint.x >= gridSize.x || refreshPoint.y >= gridSize.y || refreshPoint.z >= gridSize.z)
						{
							continue;
						}

						Vector3 worldPoint = start + (refreshPoint * nodeDiameter) + (nodeRadius * Vector3.one);
						bool navigable = !Physics.CheckBox(worldPoint, new Vector3(nodeRadius, nodeRadius, nodeRadius), Quaternion.identity, unnavigableLayer);
						float penalty = !navigable ? maxMovementpenalty : 0;
						Vector3Int refreshPointint = Vector3Int.FloorToInt(refreshPoint);
						grid[refreshPointint.x, refreshPointint.y, refreshPointint.z] = new AStarNode(navigable, worldPoint, refreshPointint, penalty);
					}
				}
			}
			UpdateBlur(pointNode.gridPosition, space, blurAmount);
		}

		public void UpdateBlur(Vector3Int point, int space, int blurSize)
		{
			int kernelSize = (blurSize * 2) + 1; //3
			int kernelExtents = (kernelSize - 1) / 2; //2
			int length = (space - 1) / 2; //2
			List<float> penatly = new List<float>();
			List<AStarNode> penatlynodes = new List<AStarNode>();

			for (int xl = -length; xl <= length; xl++)
			{
				for (int xy = -length; xy <= length; xy++)
				{
					for (int xz = -length; xz <= length; xz++)
					{
						Vector3 refreshPoint = point + new Vector3(xl, xy, xz);
						Vector3Int refreshPointint = Vector3Int.FloorToInt(refreshPoint);
						if (refreshPoint.x < 0 || refreshPoint.y < 0 || refreshPoint.z < 0 || refreshPoint.x >= gridSize.x || refreshPoint.y >= gridSize.y || refreshPoint.z >= gridSize.z)
						{
							continue;
						}
						AStarNode pointNode = grid[refreshPointint.x, refreshPointint.y, refreshPointint.z];
						float penaltySum = 0;
						int neighbourCount = 0;
						for (int x = -kernelExtents; x <= kernelExtents; x++)
						{
							for (int y = -kernelExtents; y <= kernelExtents; y++)
							{
								for (int z = -kernelExtents; z <= kernelExtents; z++)
								{
									int xValue = Mathf.Clamp(x + pointNode.gridPosition.x, 0, gridSize.x - 1);
									int yValue = Mathf.Clamp(y + pointNode.gridPosition.y, 0, gridSize.y - 1);
									int zValue = Mathf.Clamp(z + pointNode.gridPosition.z, 0, gridSize.z - 1);

									penaltySum += grid[xValue, yValue, zValue].movementPenalty;
									neighbourCount++;
								}
							}
						}
						penaltySum /= (kernelSize * kernelSize * kernelSize);
						penatly.Add(penaltySum);
						penatlynodes.Add(pointNode);
					}
				}
			}

			foreach (AStarNode node in penatlynodes)
			{
				node.modifiedMovementPenalty = penatly[0];
				penatly.RemoveAt(0);
			}


		}

		private void BlurGrid(int blurSize) //1
		{
			int kernelSize = (blurSize * 2) + 1; //3
			int kernelExtents = (kernelSize - 1) / 2; //2

			float[,,] blurredGrid = new float[gridSize.x, gridSize.y, gridSize.z];

			foreach (AStarNode node in grid)
			{
				float penaltySum = 0;
				int neighbourCount = 0;
				for (int x = -kernelExtents; x <= kernelExtents; x++)
				{
					for (int y = -kernelExtents; y <= kernelExtents; y++)
					{
						for (int z = -kernelExtents; z <= kernelExtents; z++)
						{
							int xValue = Mathf.Clamp(x + node.gridPosition.x, 0, gridSize.x - 1);
							int yValue = Mathf.Clamp(y + node.gridPosition.y, 0, gridSize.y - 1);
							int zValue = Mathf.Clamp(z + node.gridPosition.z, 0, gridSize.z - 1);

							penaltySum += grid[xValue, yValue, zValue].movementPenalty;
							neighbourCount++;
						}
					}
				}
				penaltySum /= (kernelSize * kernelSize * kernelSize);
				blurredGrid[node.gridPosition.x, node.gridPosition.y, node.gridPosition.z] = penaltySum;

			}
			foreach (AStarNode node in grid)
			{
				node.modifiedMovementPenalty = blurredGrid[node.gridPosition.x, node.gridPosition.y, node.gridPosition.z];
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
			foreach (AStarNode aStarNode in grid)
			{
				if (aStarNode == null)
				{
					continue;
				}
				/*if (!aStarNode.navigable)
				{
					Gizmos.DrawCube(aStarNode.worldPostion, Vector3.one);
				}*/
				if (aStarNode.modifiedMovementPenalty > 0)
				{
					Gizmos.color = new Color(1, 0, 0, aStarNode.modifiedMovementPenalty / maxMovementpenalty);
					Gizmos.DrawCube(aStarNode.worldPostion, Vector3.one);
				}
			}

			Gizmos.color = Color.red;
		}
	}
}
