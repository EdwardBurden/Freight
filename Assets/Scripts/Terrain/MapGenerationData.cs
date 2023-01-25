using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
	public class MapGenerationData
	{
		public List<IslandGenerationData> islands;

		public MapGenerationData(List<IslandGenerationData> islands)
		{
			this.islands = islands;
		}

		public bool AllBlocksPlaced()
		{
			foreach (IslandGenerationData islandGenerationData in islands)
			{
				if (!islandGenerationData.PlacedAllBlocks())
				{
					return false;
				}
			}
			return true;
		}

		public List<IslandGenerationData> IslandsWithUnplacedBlocks()
		{
			List<IslandGenerationData> islands = new List<IslandGenerationData>();
			foreach (IslandGenerationData islandGenerationData in this.islands)
			{
				if (!islandGenerationData.PlacedAllBlocks())
				{
					islands.Add(islandGenerationData);
				}
			}
			return islands;
		}


	}
}