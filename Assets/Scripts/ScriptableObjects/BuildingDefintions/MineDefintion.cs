using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/BuildingDefintion/MineDefintion")]
public class MineDefintion : BuildingDefintion
{
	[SerializeField] public ResourceDefintion resourceProduced;
	[SerializeField] public int amountProducedPerSeason;
	[SerializeField] public int startingStorage;

	public bool CanProducesResource(ResourceDefintion resourceData)
	{
		if (resourceProduced == resourceData)
		{
			return true;
		}
		return false;
	}
}
