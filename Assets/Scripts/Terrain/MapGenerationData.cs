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
	}
}