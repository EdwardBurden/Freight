using ActionLists;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBuildingFreedomAction : Action
{
	private LevelBuilding building;
	private bool freedom;

	public SetBuildingFreedomAction(LevelBuilding building, bool freedom) : base(false)
	{
		this.building = building;
		this.freedom = freedom;
	}

	public override void OnInitialise()
	{
		building.SetBuildingFreedom(freedom);
		base.OnInitialise();
	}

	public override void OnUpdate()
	{
		isFinished = true;
	}
}
