using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/BuildingDefintion/SettlementDefintion")]
public class SettlementDefintion : BuildingDefintion
{
	[SerializeField] public ResourceDefintion[] resourcesAccepted;

	public bool AcceptsResource(ResourceDefintion resourceData)
	{
		for (int i = 0; i < resourcesAccepted.Length; i++)
		{
			if (resourcesAccepted[i] == resourceData)
			{
				return true;
			}
		}
		return false;
	}
}
