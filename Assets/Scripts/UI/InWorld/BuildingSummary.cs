using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;

namespace UI
{
	public class BuildingSummary : MonoBehaviour
	{
		[SerializeField] private RequiredCollapsedResourceWidget collapsedResourceWidget;
		[SerializeField] private Transform wantsTransform;
		[SerializeField] private Transform needsTransform;
		[SerializeField] protected TMP_Text buildingNameText;

		protected LevelBuilding building;

		public void Init(LevelBuilding building)
		{
			this.building = building;
			if (building.storage != null)
			{
				building.storage.StorageUpdatedEvent += OnStorageUpdatedEvent;
			}
			Show();
		}

		public void Show()
		{
			buildingNameText.text = building.GetInteractableObjectName();
			buildingNameText.text = $"{building.GetInteractableObjectName()}"; ;

			ShowRequests(building.needs, needsTransform);
			ShowRequests(building.wants, wantsTransform);
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

		private void OnStorageUpdatedEvent()
		{
			Show();
		}

		public void OnBuildingHover()
		{
			Show();
			//throw new NotImplementedException();
		}

		public void OffBuildingHover()
		{
			Show();
			//throw new NotImplementedException();
		}

		private void OnDestroy()
		{
			if (building.storage != null)
			{
				building.storage.StorageUpdatedEvent -= OnStorageUpdatedEvent;
			}
		}
	}
}
