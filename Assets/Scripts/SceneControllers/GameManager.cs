using System;
using System.Collections;
using System.Collections.Generic;
using Terrain;
using UI;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager> //but must b destoryed when leavign game scene
{
	[SerializeField]
	private CameraController  cameraController;

	[SerializeField]
	private ActionPreviewController actionPreviewController;

	[SerializeField]
	private ShipSelectionController shipSelector;

	[SerializeField]
	private GameObject startShipPrefab;

	[SerializeField]
	private MapGenerator mapGenerator;

	[SerializeField]
	private BuildingController buildingController;

	[SerializeField]
	private SeasonsController seasonsController;

	[SerializeField]
	private GameUIController gameUIController;

	[SerializeField]
	private MainMenuUIController mainMenuUIController;



	private MapGenerationData gameGenerationData;


	private void Awake()
	{
		_instance = this;
	}

	private void Start()
	{
		GoToMainMenu();
	}

	public void GoToMainMenu()
	{
		cameraController.enabled = false;
		UIManager.instance.SetController(mainMenuUIController);
	}

	public void EndGame() {
		//seasonsController rest
		//buildingController reset
		//SetInputToSelectionMode();
	}

	public void StartNewGame()
	{
		seasonsController.StartNextSeason();
		gameGenerationData = mapGenerator.Generate();
		buildingController.DistributeBuildings(gameGenerationData);
		testships();
		SetInputToSelectionMode();
		cameraController.enabled = true;
		UIManager.instance.SetController(gameUIController);
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
		//UIManager.instance.ClearAllUI();
	}

	public void SetInputToPreviewMode()
	{
		actionPreviewController.enabled = true;
		shipSelector.enabled = false;
		buildingController.enabled = false;
	}


	public void SetInputToSelectionMode()
	{
		actionPreviewController.enabled = false;
		shipSelector.enabled = true;
		buildingController.enabled = true;

	}

	internal void OnExitGameSelected()
	{
		AppManager.instance.CloseApplication();
	}

	internal void OnNewGameSelected()
	{
		StartNewGame();
	}
}
