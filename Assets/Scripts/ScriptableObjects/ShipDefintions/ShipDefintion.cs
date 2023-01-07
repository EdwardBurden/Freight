using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public abstract class ShipDefintion : ScriptableObject
{
	[SerializeField] public string shipName;
	[SerializeField] public Sprite shipIcon;

	[SerializeField] public int maxSpeed;
	[SerializeField]  public int turnSpeed;

	[SerializeField] public int maxOrders;
	[SerializeField] public ShipActionDefintion[] shipActions;

	[SerializeField] public ShipSummary_InWorld summaryUIPrefab; //summary above ship
	[SerializeField] public Ship shipPrefab;
}
