using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ShipDefintion")]
public class ShipDefintion : ScriptableObject //wait on this
{
	public List<InteractionOrderDefintion> orderDefintions = new List<InteractionOrderDefintion>();
	//hold prefab and use defintion to generate ships when constructing them later
}
