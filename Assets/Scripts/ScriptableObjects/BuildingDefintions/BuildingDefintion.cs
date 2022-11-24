using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public abstract class BuildingDefintion : ScriptableObject
{
	[SerializeField] public string buildingName;
	[SerializeField] public Sprite buildingIcon;
	[SerializeField] public int buildCost = 100;

	[SerializeField] public GameObject summaryUIPrefab; //summary above building

	[SerializeField] public Building buildingPrefab;  //large ui panel of building details
}
