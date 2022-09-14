using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/MapGenerationSettings")]
public class MapGenerationSettings : ScriptableObject
{
	//General
	public string settingsName; //smal,medium,large
	public Vector2 mapSize;

	//Islands
	public int islandRadius = 10;
}
