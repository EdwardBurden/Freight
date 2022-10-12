using System.Collections.Generic;
using UnityEngine;

namespace Terrain
{
	public class IslandGenerationData
	{
		//Useful 
		public GameObject islandBase;
		public Vector2 center;
		public List<IslandBlock> blocks = new List<IslandBlock>();

		//Generation
		public List<Vector2> allRandomPoints = new List<Vector2>();
		public List<Vertex> points = new List<Vertex>();
		public List<Vertex> boundary = new List<Vertex>();
		public List<VoronoiCell> voronoiCells = new List<VoronoiCell>();
	}
}