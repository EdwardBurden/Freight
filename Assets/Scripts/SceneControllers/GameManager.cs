using System;
using Terrain;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

//TODO THIS CLASS IS A MESS, FIX LATER
public class GameManager : Singleton<GameManager>
{
	public static event Action GameStartEvent;

	[SerializeField] private CameraController cameraController;
	[SerializeField] private TimeController timeController;
	[SerializeField] private PreviewController previewController;
	[SerializeField] private ShipSelectionController shipSelector;
	[SerializeField] private TerrainController terrainController;
	[SerializeField] private BuildingController buildingController;
	[SerializeField] private NotificationsController notificationsController;
	[SerializeField] private SpawnController spawnController;
	[SerializeField] private PathfindingController pathfindingController;
	[SerializeField] private TurnsController turnRewardsController;
	[SerializeField] private WalletController walletController;

	[SerializeField] private MouseInput mouseInput;
	[SerializeField] private GameSetting gameSetting;
	[SerializeField] private GameUIController gameUIController;
	[SerializeField] private MainMenuUIController mainMenuUIController;

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
		TurnsController.TurnEndedEvent += OnTurnEnd;
		TurnsController.TurnStartedEvent += OnTurnStarted;
	}

	public void GoToMainMenu() //move to app manager
	{
		cameraController.enabled = false;
		UIManager.instance.SetController(mainMenuUIController);
	}

	public void StartNewGame()
	{
		cameraController.Init();
		timeController.Init();
		previewController.Init();
		previewController.Init();
		shipSelector.Init();
		terrainController.Init();
		buildingController.Init();
		notificationsController.Init();
		spawnController.Init();
		pathfindingController.Init();
		turnRewardsController.Init();
		walletController.Init();

		//timeController.StartTime();
		terrainController.TerrainGeneratedEvent += OnGameMapInitialised;
		terrainController.GenerateMap();
	}

	private void OnGameMapInitialised(MapGenerationData gameGenerationData)
	{
		terrainController.TerrainGeneratedEvent -= OnGameMapInitialised;
		SetupGameFromSettings();
		SetInputToSelectionMode();
		UIManager.instance.SetController(gameUIController);
		timeController.StartTime();
		GameStartEvent?.Invoke();
	}

	private void SetupGameFromSettings()
	{
		spawnController.PlaceMotherShip();
		spawnController.DistributeShips(); //TODO should eb from game settings
		for (int i = 0; i < gameSetting.startingbuildings.Length; i++)
		{
			IslandBlock islandBlock = terrainController.AddNextBlock();
			buildingController.AddBuilding(gameSetting.startingbuildings[i], islandBlock);
		}
		pathfindingController.CreateGrid();
	}

	private void OnTurnEnd(int obj)
	{
		//cameraController.enabled = false;
		timeController.PauseTime();
		//mouseInput.enabled = false;
	}

	private void OnTurnStarted(int turn)
	{
		IslandBlock islandBlock = terrainController.AddNextBlock();
		BuildingSpawnInfo buildingSpawnInfo = gameSetting.allBuildings[Random.Range(0, gameSetting.allBuildings.Length)]; //temp
		buildingController.AddBuilding(buildingSpawnInfo.building, islandBlock);
		pathfindingController.UpdatedGrid(islandBlock.transform.position, 10);

		//cameraController.enabled = true;
		timeController.ResumeTime();
		//mouseInput.enabled = true;
	}

	public void SetInputToPreviewMode()
	{
		previewController.enabled = true;
		shipSelector.enabled = false;
		buildingController.enabled = false;
	}

	public void SetInputToSelectionMode()
	{
		previewController.enabled = false;
		shipSelector.enabled = true;
		buildingController.enabled = true;
	}

	internal void ExitApp() //move to app manager
	{
		AppManager.instance.CloseApplication();
	}

	public void EndGame()  //TODO
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
