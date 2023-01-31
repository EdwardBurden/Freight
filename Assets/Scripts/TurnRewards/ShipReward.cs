using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TurnReward/ShipReward")]
public class ShipReward : TurnReward
{
	public ShipDefintion shipRewardDefintion;

	public override void OnRewardSelected(Action<bool> callback)
	{
		//move camera to ship
		//play animation of ship being spawned etc
		//Put as preview and use preivew controller
		SpawnController.instance.SpawnShip(shipRewardDefintion); //temp
		callback?.Invoke(true);
	}
}
