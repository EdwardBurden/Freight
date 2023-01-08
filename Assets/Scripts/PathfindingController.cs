using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingController : MonoBehaviour
{
	private AStarGrid aStarGrid;

	private void Awake()
	{
		aStarGrid = GetComponent<AStarGrid>();
	}


	// Start is called before the first frame update
	void Start()
	{
		//aStarGrid = AStarPathfinding.CreateGrid();

	}
}
