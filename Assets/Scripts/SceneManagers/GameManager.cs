using System.Collections;
using System.Collections.Generic;
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
		mapGenerator?.Generate();
		//Place resources of map
		//Place buildings on map
		//initialse any systmes that need map info
		//place player ship 
		var ship = Instantiate(startShipPrefab);
		ship.transform.position = new Vector3(0, 5, 0);
		UIManager.instance.EnableHUD();
		UIManager.instance.AddPanels(gameUIPanels);
		SetInputToSelectionMode();
		//UIManager. ADD GAme panels
		//add game ui to uimanager /hud, panels etc
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
