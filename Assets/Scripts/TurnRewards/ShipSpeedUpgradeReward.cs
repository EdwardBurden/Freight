using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TurnReward/ShipSpeedUpgradeReward")]
public class ShipSpeedUpgradeReward : ShipUpgradeReward
{
	public int speedIncrease;

	public override void OnRewardSelected(Action<bool> callback)
	{
		//ActionPreviewController
		//TurnRewardPreviewController.instance.
		//have user select the ship
		//if user left clicks on ship
		//apply change to ship 
		//else if they click on anything else or right click 
		//display error
	}
}
