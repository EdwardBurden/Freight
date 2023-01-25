using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
	public class IslandGenerationData
	{
		public Vector2 islandPoint;
		public List<VoronoiCell> cells;
		public List<IslandBlock> placedBlocks = new List<IslandBlock>();
		public GameObject islandBase = null;

		public IslandGenerationData(Vector2 islandPoint, List<VoronoiCell> cells)
		{
			this.islandPoint = islandPoint;
			this.cells = cells;
		}

		public VoronoiCell GetNextCell()
		{
			int index = placedBlocks.Count;
			if (index > cells.Count - 1)
			{
				return null;
			}
			return cells[index];
		}

		public bool PlacedAllBlocks()
		{
			return cells.Count == placedBlocks.Count;
		}


	}
}