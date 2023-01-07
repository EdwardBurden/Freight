using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : Singleton<SpawnController> //Could look into factory pattern here instad 
{
	[SerializeField] private int shipSpawnNumber =5; //temp
	[SerializeField] private ShipDefintion[] shipDefintions; //temp

	public void DistributeShips()
	{
		for (int i = 0; i < shipSpawnNumber; i++)
		{
			int index = Random.Range(0, shipDefintions.Length);
			Ship ship = SpawnShip(shipDefintions[index]);
			ship.transform.position = new Vector3(i * 5, 5, 0);
		}

	}


	public Ship SpawnShip(ShipDefintion shipDefintion)
	{
		Ship ship = Instantiate(shipDefintion.shipPrefab);
		ship.Init(shipDefintion);
		return ship;
	}

	public T SpawnBuilding<T>() where T : Building
	{
		//TODO
		return null;

	}
}
