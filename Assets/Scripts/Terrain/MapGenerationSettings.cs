using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/MapGenerationSettings")]
public class MapGenerationSettings : ScriptableObject
{
	[Header("Map")]
	public string settingsName; //smal,medium,large
	public Vector2 mapSize;
	public int islandRadius = 10;



	[Header("island")]
	public Vector2 islandSize;
	public int islandSpotRadius = 10;
}
