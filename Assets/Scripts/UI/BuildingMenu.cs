using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingMenu : Menu
{
	protected Building building;
	public virtual void Display(Building building)
	{
		this.building = building;
		Open();
		//throw new NotImplementedException();
	}
}
