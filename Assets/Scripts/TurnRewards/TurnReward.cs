using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public abstract class TurnReward : ScriptableObject
{
	public string rewardName;
	public string rewardDescription;
	public Sprite rewardIcon;
}
