using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terrain;

public class BuildingController : MonoBehaviour
{
	[SerializeField]
	private List<Resources> allResources = new List<Resources>();

	[SerializeField]
	private GameObject[] buildingTest;


	public void DistributeBuildings(MapGenerationData mapGenerationData)
	{
		foreach (IslandGenerationData islandGenerationData in mapGenerationData.islands)
		{
			for (int i = 0; i < islandGenerationData.blocks.Count; i++)
			{
				int index = Random.Range(0, buildingTest.Length);
				int rotation = Random.Range(0, 360);
				GameObject building = Instantiate(buildingTest[index], islandGenerationData.blocks[i].transform.GetChild(0));
				building.transform.rotation = Quaternion.Euler(0, rotation, 0);
				building.transform.localPosition = Vector3.zero;
			}
		}
	}

//	private 


	//

}
