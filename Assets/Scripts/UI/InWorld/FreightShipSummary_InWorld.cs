using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
	public class FreightShipSummary_InWorld : ShipSummary_InWorld
	{
		[SerializeField] private ResourceIconButtonWidget resourceIconButtonWidget;
		[SerializeField] private Transform cargoTransform;

		private FreightShip freightShip => ship as FreightShip;

		public override void Init(Ship ship)
		{
			base.Init(ship);
			if (freightShip.carryObject != null)
			{
				freightShip.carryObject.FreightAddedEvent += CarryObject_FreightAddedEvent; ;
				freightShip.carryObject.FreightRemovedEvent += CarryObject_FreightRemovedEvent; ;
			}
		}

		private void CarryObject_FreightRemovedEvent(FreightObject obj)
		{
			Show();
		}

		private void CarryObject_FreightAddedEvent(FreightObject obj)
		{
			Show();
		}

		public override void Show()
		{
			base.Show();
			GameObjectUtils.Clear(cargoTransform);
			foreach (FreightObject freightObject in freightShip.GetAllFreight())
			{
				ResourceIconButtonWidget resourceIconButton = Instantiate(resourceIconButtonWidget, cargoTransform);
				resourceIconButton.ShowResource(freightObject.resourceData);
			}
		}

		private void OnDestroy()
		{
			if (freightShip.carryObject != null)
			{
				freightShip.carryObject.FreightAddedEvent -= CarryObject_FreightAddedEvent; ;
				freightShip.carryObject.FreightRemovedEvent -= CarryObject_FreightRemovedEvent; ;
			}
		}

	}
}