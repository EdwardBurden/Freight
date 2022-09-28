using System.Collections;
using System.Collections.Generic;
using Terrain;
using UI;
using UnityEngine;

public class GameManager : Singleton<GameManager> //but must b destoryed when leavign game scene
{
	[SerializeField]
	private ActionPreviewController actionPreviewController;

	[SerializeField]
	private ShipSelectionController shipSelector;

	[SerializeField]
	private GameObject startShipPrefab;

	[SerializeField]
	private List<Panel> gameUIPanels = new List<Panel>();

	[SerializeField]
	private MapGenerator mapGenerator;

	[SerializeField]
	private GameObject[] buildingTest;


	public bool startlocal = false;

	private void Awake()
	{
		_instance = this;
	}

	private void Start()
	{
		if (startlocal)
			StartNewGame();
	}

	public void StartNewGame()
	{
		//Setup Game world
		MapGenerationData mapGenerationData = mapGenerator?.Generate();
		testBuildings(mapGenerationData);
		testships();
		//Place resources of map
		//Place buildings on map
		//initialse any systmes that need map info
		//place player ship 

		//SetUp UI
		UIManager.instance.EnableHUD();
		UIManager.instance.AddPanels(gameUIPanels);
		//SetUpInput
		SetInputToSelectionMode();
	}

	private void testBuildings(MapGenerationData mapGenerationData) //maybe go to buildingmanager class to make them?
	{
		foreach (IslandGenerationData islandGenerationData in mapGenerationData.islands)
		{
			for (int i = 0; i < islandGenerationData.cells.Count; i++)
			{
				int index = Random.Range(0, buildingTest.Length);
				int rotation = Random.Range(0, 360);
				GameObject building = Instantiate(buildingTest[index], islandGenerationData.cells[i].transform.GetChild(0));
				building.transform.rotation = Quaternion.Euler(0, rotation, 0);
				building.transform.localPosition = Vector3.zero;
			}
		}

	}

	private void testships() {
		for (int i = 0; i < 5; i++)
		{
			var ship = Instantiate(startShipPrefab);
			ship.transform.position = new Vector3(i*5, 5, 0);
		}
	
	}

	public void ExitGame()
	{
		//do this , do that
		//etc
		UIManager.instance.ClearAllUI();
	}

	public void SetInputToPreviewMode()
	{
		actionPreviewController.enabled = true;
		shipSelector.enabled = false;
	}


	public void SetInputToSelectionMode()
	{
		actionPreviewController.enabled = false;
		shipSelector.enabled = true;

	}
}
