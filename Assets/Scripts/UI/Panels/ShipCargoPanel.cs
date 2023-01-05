using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UI
{
	public class ShipCargoPanel : Panel
	{
		[SerializeField] private Transform resourcesListTransform;
		[SerializeField] private ResourceIconButtonWidget resourceIconButtonWidgetPrefab;
		[SerializeField] private TMP_Text sizeText;

		private CarryObject carryObject;

		protected void OnEnable()
		{
			ShipSelectionController.ShipSelectedEvent += OnShipSelected;
			ShipSelectionController.ShipDeSelectedEvent += OnShipDeselected;
		}
		private void OnDisable()
		{
			ShipSelectionController.ShipSelectedEvent -= OnShipSelected;
			ShipSelectionController.ShipDeSelectedEvent -= OnShipDeselected;
		}

		private void OnShipSelected(Ship shipObject)
		{
			Hide();
			CarryObject carryObject = shipObject.GetComponent<CarryObject>();
			if (carryObject == null)
			{
				return;
			}
			this.carryObject = carryObject;
			Show();
		}

		private void OnShipDeselected(Ship obj)
		{
			Hide();
		}

		public override void Show()
		{
			carryObject.FreightAddedEvent += CarryObject_FreightAddedEvent;
			carryObject.FreightRemovedEvent += CarryObject_FreightRemovedEvent;
			Refresh();
			base.Show();
		}

		private void Refresh()
		{
			GameObjectUtils.Clear(resourcesListTransform);
			foreach (FreightObject freightObject in carryObject.GetAllFreight())
			{
				ResourceIconButtonWidget resourceIconButton = Instantiate(resourceIconButtonWidgetPrefab, resourcesListTransform);
				resourceIconButton.ShowResource(freightObject.resourceData);
			}
			sizeText.text = $"{carryObject.GetStorageUsed()}/{carryObject.GetMaxStorage()}";
		}

		private void CarryObject_FreightRemovedEvent(FreightObject obj)
		{
			Refresh();
		}

		public override void Hide()
		{
			base.Hide();
			if (carryObject != null)
			{
				carryObject.FreightAddedEvent -= CarryObject_FreightAddedEvent;
				carryObject.FreightRemovedEvent -= CarryObject_FreightRemovedEvent;
			}
		}

		private void CarryObject_FreightAddedEvent(FreightObject obj)
		{
			Refresh();
		}

		protected void Clear(Transform transform)
		{
			foreach (Transform item in transform)
			{
				Destroy(item.gameObject);
			}
		}
	}
}