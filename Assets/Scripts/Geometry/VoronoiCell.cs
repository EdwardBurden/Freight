using System.Collections.Generic;
using UnityEngine;

public class VoronoiCell
{
    //All positions within a voronoi cell is closer to this position than any other position in the diagram
    public Vector3 sitePos;

    public List<Vector3> borderCoordinates = new List<Vector3>();

    public List<VoronoiEdge> edges = new List<VoronoiEdge>();

    public VoronoiCell(Vector3 sitePos)
    {
        this.sitePos = sitePos;
    }
}
