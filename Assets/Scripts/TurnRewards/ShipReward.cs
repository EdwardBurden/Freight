using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TurnReward/ShipReward")]
public class ShipReward : TurnReward
{
	public ShipDefintion shipRewardDefintion;
	public ShipRewardPreview previewPrefab;

	public override void OnRewardSelected(Action<bool> callback)
	{
		ShipRewardPreview gamePreview = Instantiate(previewPrefab);
		PreviewController.instance.SetPreview(gamePreview);
		gamePreview.Init(shipRewardDefintion, callback);
	}
}
