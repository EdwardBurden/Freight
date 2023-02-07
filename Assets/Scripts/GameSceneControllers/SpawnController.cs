using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TEMP, can delete later
public class SpawnController : Singleton<SpawnController>, IGameController //Could look into factory pattern here instad 
{
	[SerializeField] private int shipSpawnNumber = 5; //temp
	[SerializeField] private ShipDefintion[] shipDefintions; //temp
	[SerializeField] private GameObject motherShip;

	private void Awake()
	{
		_instance = this;
	}

	public void Init()
	{
	//
	}

	public void DistributeShips()
	{
		for (int i = 0; i < shipSpawnNumber; i++)
		{
			int index = Random.Range(0, shipDefintions.Length);
			Ship ship = shipDefintions[index].CreateShip();
			ship.transform.position = new Vector3(i * 5, 5, 0);
		}
	}

	public void PlaceMotherShip() //TEMP
	{
		Vector3 pos = TerrainController.instance.FindAvailablePlaceOnTerrain();
		Instantiate(motherShip, pos, Quaternion.identity);
	}
}
