using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTerrainUI : MonoBehaviour
{
	private const int MAP_SIZE_MAX = 50;

	[SerializeField]
	public MapGenerator mapGenerator;
	[SerializeField]
	public Slider sizeSlider; //0 to 1


	public void Generate()
	{
		int size = (int)(sizeSlider.value * MAP_SIZE_MAX);
		mapGenerator.Generate(size, size);

	}
}
