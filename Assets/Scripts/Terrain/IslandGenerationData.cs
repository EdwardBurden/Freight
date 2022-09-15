using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerationData
{
    public Vector2 center;
    public List<Vector2> spots = new List<Vector2>();

    //Temp
   public List<Vertex> points = new List<Vertex>();
    public List<Vertex> boundary = new List<Vertex>();
    public List<VoronoiCell> voronoiCells = new List<VoronoiCell>(); 

}
