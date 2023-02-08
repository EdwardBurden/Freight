using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Terrain
{
	public class IslandGenerationData
	{
		public Vector2 islandPoint;
		public List<VoronoiCell> cells;
		public List<Vertex> boundary;
		public List<IslandBlock> placedBlocks = new List<IslandBlock>();
		public GameObject islandBase = null;

		public IslandGenerationData(Vector2 islandPoint, List<VoronoiCell> cells, List<Vertex> boundary)
		{
			this.islandPoint = islandPoint;
			this.cells = cells;
			this.boundary = boundary;
		}

		public List<VoronoiCell> GetUnplacedCells()
		{
			return cells.Where(x => !placedBlocks.Any(placedBlocks => placedBlocks.sitePostion == x.sitePos)).ToList();
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