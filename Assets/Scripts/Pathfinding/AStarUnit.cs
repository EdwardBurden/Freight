using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AStar
{
	//TODO could be  IPathfiningUnit instead TODO
	public class AStarUnit : MonoBehaviour
	{
		//Subsribed to in navigate action initilaise
		//unsubsribed to when event fires or if action in cancelled
		public event Action CompletedPathMovementEvent;

		//Called form naviagte action
		public void MoveOnPath(List<AStarNode> path)
		{


		}
	}
}