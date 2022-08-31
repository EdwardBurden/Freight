using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public List<GameObject> islandBases;

	public List<GameObject> islandBlocks;

	public int islandsAmount;
	public int maxBlocks;
	public float minIslandDistance;
	public Vector2 mapSize;


	private void Start()
	{
		List<GameObject> islands = new List<GameObject>();

		for (int i = 0; i < islandsAmount; i++)
		{
			int baseIndex = Random.Range(0, islandBases.Count);
			GameObject islandBase = Instantiate(islandBases[baseIndex]);

			bool found = false;
			int counter = 0;
			Vector3 position = Vector3.one;
			while (!found && counter < 100)
			{
				found = true;
				float x = Random.Range(-mapSize.x, mapSize.x);
				float z = Random.Range(-mapSize.y, mapSize.y);
				Vector3 pos = new Vector3(x, 0, z);
				foreach (var item in islands)
				{
					if (Vector3.Distance(item.transform.position, pos) < minIslandDistance)
					{
						found = false;
					}
				}
				if (found) {
					position = pos;
				}
				counter++;
			}


			float degree = Random.Range(0.0f, 360.0f);
			islandBase.transform.position = position;
			islandBase.transform.rotation = Quaternion.Euler(0, degree, 0);
			islands.Add(islandBase);
		}
	}
}
