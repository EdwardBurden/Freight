using System;
using System.Collections;
using System.Collections.Generic;
using Terrain;
using UI;
using UnityEngine;

public abstract class BuildingLevelDefintion : ScriptableObject
{
	[SerializeField] public string buildingName;
	[SerializeField] public Sprite buildingIcon;
	[SerializeField] public int buildCost = 100;

	[SerializeReference, SerializeReferenceButton] public ResourceRequestDefintion[] wants;
	[SerializeReference, SerializeReferenceButton] public ResourceRequestDefintion[] needs;

	[SerializeField] public BuildingSummary_InWorld summaryUIPrefab; //summary above building
	[SerializeField] public LevelBuilding buildingPrefab;

	internal ResourceRequest[] CreateNeeds()
	{
		List<ResourceRequest> resourceRequests = new List<ResourceRequest>();
		foreach (ResourceRequestDefintion requestDefintion in needs)
		{
			ResourceRequest resourceRequest = requestDefintion.MakeRequest();
			resourceRequests.Add(resourceRequest);
		}
		return resourceRequests.ToArray();
	}
}
