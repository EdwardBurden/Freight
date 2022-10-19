using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForBuildingToBeFreeAction : Action
{
	private Building building;

	public WaitForBuildingToBeFreeAction(Building building) : base(true)
	{
		this.building = building;
	}

	public override void OnUpdate()
	{
		if (building.IsFree())
		{
			isFinished = true;
		}
	}
}
