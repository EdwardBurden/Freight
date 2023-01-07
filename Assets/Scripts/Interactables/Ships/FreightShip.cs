using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarryObject))]
public class FreightShip : Ship
{
	[HideInInspector] public CarryObject carryObject;

	public override void Init(ShipDefintion shipDefintion)
	{
		carryObject = GetComponent<CarryObject>();
		base.Init(shipDefintion);
	}

	public List<FreightObject> GetAllFreight()
	{
		return carryObject.GetAllFreight();
	}
}
