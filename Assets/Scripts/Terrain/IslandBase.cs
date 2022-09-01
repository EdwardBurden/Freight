using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandBase : MonoBehaviour
{
	[SerializeField]
	private float blockSpawnRadius = 5;


	public List<IslandBlock> islandBlocks = new List<IslandBlock>();
}
