using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Building")]
public class BuildingDefintion : ScriptableObject
{
	[SerializeField] public Building buildingBase;
	[SerializeField] public BuildingLevelDefintion[] buildingLevels; 
}
