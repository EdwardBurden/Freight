using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

namespace UI
{
	public class BuildingPanel : Panel
	{
		[SerializeField] private TMP_Text nameField;
		[SerializeField] private Transform wantRequests;
		[SerializeField] private Transform needRequests;
		[SerializeField] private RequiredCollapsedResourceWidget collapsedResourceWidget;

		protected Building building;
		protected Storage buildingStorage;

		protected void OnEnable()
		{
			BuildingController.BuildingSelectedEvent += OnBuildingSelected;
			BuildingController.BuildingDeSelectedEvent += OnBuildingDeSelected;
		}

		private void OnDisable()
		{
			BuildingController.BuildingSelectedEvent -= OnBuildingSelected;
			BuildingController.BuildingDeSelectedEvent -= OnBuildingDeSelected;
		}

		protected virtual void OnBuildingDeSelected(Building building)
		{
			Hide();
			building = null;
		}

		protected virtual void OnBuildingSelected(Building building)
		{
			Hide();
			this.building = building;
			Show();
		}

		public override void Show()
		{
			base.Show();
			nameField.text = building.GetInteractableObjectName();
			if (buildingStorage != building.storage && building.storage != null)
			{
				buildingStorage = building.storage;
				buildingStorage.StorageUpdatedEvent += OnStorageUpdated;
			}
			ShowRequests(building.GetCurrentLevel().needs, needRequests);
			ShowRequests(building.GetCurrentLevel().wants, wantRequests);
		}

		protected virtual void OnStorageUpdated()
		{
			//temp 
			Show();
		}

		private void ShowRequests(RequestGroup requestGroup, Transform parent)
		{
			GameObjectUtils.Clear(parent);
			if (requestGroup == null || requestGroup.resourceRequests == null)
			{
				return;
			}
			foreach (ResourceRequest resourceRequest in requestGroup.resourceRequests)
			{
				RequiredCollapsedResourceWidget requiredCollapsedResource = Instantiate(collapsedResourceWidget, parent);
				requiredCollapsedResource.ShowResource(resourceRequest);
			}
		}

		public override void Hide()
		{
			base.Hide();
			if (buildingStorage != null)
			{
				buildingStorage.StorageUpdatedEvent -= OnStorageUpdated;
			}
		}
	}
}