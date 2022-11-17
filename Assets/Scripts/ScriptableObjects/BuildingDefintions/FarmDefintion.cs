using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/BuildingDefintion/FarmDefintion")]
public class FarmDefintion : BuildingDefintion
{
	[SerializeField] public ResourceDefintion resourceProduced;
	[SerializeField] public int amountProducedPerAutumn;
	[SerializeField] public int amountLostPerSeason;

	public bool CanProducesResource(ResourceDefintion resourceData)
	{
		if (resourceProduced == resourceData )
		{
			return true;
		}
		return false;
	}
}
