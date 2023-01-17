using System;
using Terrain;
using UI;
using UnityEngine;

public class GameManager : Singleton<GameManager> //but must b destoryed when leavign game scene
{
	public static event Action GameStartEvent;

	[SerializeField] private CameraController cameraController;
	[SerializeField] private TimeController timeController;
	[SerializeField] private ActionPreviewController actionPreviewController;
	[SerializeField] private ShipSelectionController shipSelector;
	[SerializeField] private MapGenerator mapGenerator;
	[SerializeField] private BuildingController buildingController;
	[SerializeField] private SeasonsController seasonsController;
	[SerializeField] private GameUIController gameUIController;
	[SerializeField] private MainMenuUIController mainMenuUIController;
	[SerializeField] private NotificationsController notificationsController;
	[SerializeField] private SpawnController spawnController;
	[SerializeField] private PathfindingController pathfindingController;

	private MapGenerationData gameGenerationData;

	private void Awake()
	{
		_instance = this;
	}

	private void Start()
	{
		//	GoToMainMenu();
		StartNewGame();
	}

	public void GoToMainMenu()
	{
		cameraController.enabled = false;
		UIManager.instance.SetController(mainMenuUIController);
	}

	public void EndGame()
	{
		mapGenerator.ClearMap();
		foreach (var item in FindObjectsOfType<Ship>())
		{
			Destroy(item.gameObject);
		}
		timeController.ResumeTime();
		//remove ships and rest ship selecter
		//remove buildings and reset building controller
	}

	public void StartNewGame()
	{
		cameraController.enabled = true;
		timeController.StartTime();

		gameGenerationData = mapGenerator.Generate();
		buildingController.DistributeBuildings(gameGenerationData);
		spawnController.DistributeShips();
		notificationsController.Init();
		SetInputToSelectionMode();
		UIManager.instance.SetController(gameUIController);
		seasonsController.Init();
		pathfindingController.Init();
		GameStartEvent?.Invoke();
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
