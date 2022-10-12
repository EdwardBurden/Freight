using UnityEngine;

public abstract class Building : InteractableObject
{
	[SerializeField]
	private string buildingName;

	public override string GetObjectName()
	{
		return buildingName;
	}
}
