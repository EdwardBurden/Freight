using System;
using Terrain;
using UI;
using UnityEngine;

//TODO THIS CLASS IS A MESS, FIX LATER
public class GameManager : Singleton<GameManager> 
{
	public static event Action GameStartEvent;

	[SerializeField] private CameraController cameraController;
	[SerializeField] private TimeController timeController;
	[SerializeField] private PreviewController actionPreviewController;
	[SerializeField] private ShipSelectionController shipSelector;
	[SerializeField] private TerrainController terrainController;
	[SerializeField] private BuildingController buildingController;
	[SerializeField] private GameUIController gameUIController;
	[SerializeField] private MainMenuUIController mainMenuUIController;
	[SerializeField] private NotificationsController notificationsController;
	[SerializeField] private SpawnController spawnController;
	[SerializeField] private PathfindingController pathfindingController;
	[SerializeField] private TurnRewardsController turnRewardsController;
	[SerializeField] private MouseInput mouseInput;

	private void Awake()
	{
		_instance = this;
	}

	private void Start()
	{
		//GoToMainMenu();
		StartNewGame();
	}

	private void OnEnable()
	{
		TurnRewardsController.TurnEndedEvent += OnTurnEnd;
		TurnRewardsController.TurnStartedEvent += OnTurnStarted;
	}

	public void GoToMainMenu() //move to app manager
	{
		cameraController.enabled = false;
		UIManager.instance.SetController(mainMenuUIController);
	}

	public void StartNewGame()
	{
		cameraController.enabled = true;
		timeController.StartTime();
		terrainController.TerrainGeneratedEvent += OnTerrainGenerated;
		terrainController.GenerateMap();
	}

	private void OnTerrainGenerated(MapGenerationData gameGenerationData)
	{
		//buildingController.DistributeBuildings(gameGenerationData);
		terrainController.TerrainGeneratedEvent -= OnTerrainGenerated;
		spawnController.DistributeShips();
		notificationsController.Init();
		SetInputToSelectionMode();
		UIManager.instance.SetController(gameUIController);
		//seasonsController.Init();
		pathfindingController.Init();
		turnRewardsController.Init();
		turnRewardsController.StartTurn();
		GameStartEvent?.Invoke();
	}

	private void OnTurnEnd(int obj)
	{
		//cameraController.enabled = false;
		timeController.PauseTime();
		//mouseInput.enabled = false;
	}


	private void OnTurnStarted(int turn)
	{
		//cameraController.enabled = true;
		timeController.ResumeTime();
		//mouseInput.enabled = true;
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

	internal void ExitApp() //move to app manager
	{
		AppManager.instance.CloseApplication();
	}

	public void EndGame()  //move to app manager
	{
		//mapGenerator.ClearMap();
		foreach (var item in FindObjectsOfType<Ship>())
		{
			Destroy(item.gameObject);
		}
		timeController.ResumeTime();
		//remove ships and rest ship selecter
		//remove buildings and reset building controller
	}
}
