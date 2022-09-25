using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> //but must b destoryed when leavign game scene
{
	[SerializeField]
	private MapGenerator mapGenerator;

	private void Awake()
	{
		_instance = this;
	}

	public void StartNewGame()
	{
		mapGenerator.Generate();
		//Place resources of map
		//Place buildings on map
		//initialse any systmes that need map info
		//place player ship 
		UIManager.instance.EnableHUD();
		//UIManager. ADD GAme panels
		//add game ui to uimanager /hud, panels etc
	}

	public void ExitGame()
	{
		//do this , do that
		//etc
		UIManager.instance.ClearAllUI();


	}
}
