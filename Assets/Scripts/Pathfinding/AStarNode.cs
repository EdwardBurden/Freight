using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNode
{
	public bool navigable;
	public Vector3 worldPostion;

	public AStarNode(bool navigable, Vector3 worldPostion)
	{
		this.navigable = navigable;
		this.worldPostion = worldPostion;
	}
}
