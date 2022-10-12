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
	private BuildingController buildingController;


	public bool startlocal = false;

	private MapGenerationData gameGenerationData;


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
		gameGenerationData = mapGenerator.Generate();
		buildingController.DistributeBuildings(gameGenerationData);
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

	private void testships()
	{
		for (int i = 0; i < 5; i++)
		{
			var ship = Instantiate(startShipPrefab);
			ship.transform.position = new Vector3(i * 5, 5, 0);
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
