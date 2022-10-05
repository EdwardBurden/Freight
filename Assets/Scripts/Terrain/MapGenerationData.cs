using System.Collections.Generic;

namespace Terrain
{
	public class MapGenerationData
	{
		public MapGenerationSettings settings;
		public List<IslandGenerationData> islands;

		public MapGenerationData(MapGenerationSettings settings)
		{
			this.settings = settings;
		}
	}
}