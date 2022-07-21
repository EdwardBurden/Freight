using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : InteractableObject
{
	private GroundCell groundCell; //info for cell the building is on, assigned when building is selected to be here
}
