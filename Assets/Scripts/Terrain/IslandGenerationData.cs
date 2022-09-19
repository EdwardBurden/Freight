using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerationData
{
	//Useful 
	public Vector2 center;
	public List<GameObject> cells = new List<GameObject>();

	//Generation
	public List<Vector2> allRandomPoints = new List<Vector2>();
	public List<Vertex> points = new List<Vertex>();
	public List<Vertex> boundary = new List<Vertex>();
	public List<VoronoiCell> voronoiCells = new List<VoronoiCell>();
}
