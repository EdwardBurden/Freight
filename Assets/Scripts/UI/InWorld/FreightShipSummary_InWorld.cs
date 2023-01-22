using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
	public class FreightShipSummary_InWorld : ShipSummary_InWorld
	{
		[SerializeField] private ResourceIconButtonWidget resourceIconButtonWidget;
		[SerializeField] private Transform cargoTransform;

		private CarryObject carryObject => ship.GetComponent<CarryObject>();

		public override void Init(Ship ship)
		{
			base.Init(ship);
			if (carryObject != null)
			{
				carryObject.FreightAddedEvent += CarryObject_FreightAddedEvent; ;
				carryObject.FreightRemovedEvent += CarryObject_FreightRemovedEvent; ;
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
			foreach (FreightObject freightObject in carryObject.GetAllFreight())
			{
				ResourceIconButtonWidget resourceIconButton = Instantiate(resourceIconButtonWidget, cargoTransform);
				resourceIconButton.ShowResource(freightObject.resourceData);
			}
		}

		private void OnDestroy()
		{
			if (carryObject != null)
			{
				carryObject.FreightAddedEvent -= CarryObject_FreightAddedEvent; ;
				carryObject.FreightRemovedEvent -= CarryObject_FreightRemovedEvent; ;
			}
		}

	}
}