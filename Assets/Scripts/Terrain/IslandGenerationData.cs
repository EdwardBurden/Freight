using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
	public class IslandGenerationData
	{
		//new
		public Vector2 islandPoint;
		public List<Vector2> allBlockPoints; //can remove
		public List<VoronoiCell> cells;

		//OLD
		//Useful 
		public GameObject islandBase;
		public Vector2 center;
		public List<IslandBlock> blocks = new List<IslandBlock>();

		//Generation 
		public List<Vector2> allRandomPoints = new List<Vector2>();
		public List<Vertex> points = new List<Vertex>();
		public List<Vertex> boundary = new List<Vertex>();
		public List<VoronoiCell> voronoiCells = new List<VoronoiCell>();

		public IslandGenerationData(Vector2 islandPoint, List<Vector2> blockPoints, List<VoronoiCell> cells)
		{
			this.islandPoint = islandPoint;
			this.allBlockPoints = blockPoints;
			this.cells = cells;
		}
	}
}