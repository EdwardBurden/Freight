using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TurnReward/ShipSpeedUpgradeReward")]
public class ShipSpeedUpgradeReward : ShipUpgradeReward
{
	public int speedIncrease;
	public ShipUpgradePreview previewPrefab;

	public override void OnRewardSelected(Action<bool> callback)
	{
		ShipUpgradePreview gamePreview = Instantiate(previewPrefab);
		PreviewController.instance.SetPreview(gamePreview);
		gamePreview.Init(speedIncrease, (ship) => OnShipSelected(ship, callback));
		//ActionPreviewController
		//TurnRewardPreviewController.instance.
		//have user select the ship
		//if user left clicks on ship
		//apply change to ship 
		//else if they click on anything else or right click 
		//display error
	}

	private void OnShipSelected(Ship ship, Action<bool> callback)
	{
		ship.ModifySpeed(speedIncrease);
		callback?.Invoke(true);
	}
}
