using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GameSetting")]
public class GameSetting : ScriptableObject
{
	public BuildingSpawnInfo[] allBuildings;
	public BuildingDefintion[] startingbuildings; //also starting island number
}

[Serializable]
public class BuildingSpawnInfo
{
	public BuildingDefintion building;
	public float spawnChance;
	public int maxSpawnAmount; //check in buildingcontroller
}
