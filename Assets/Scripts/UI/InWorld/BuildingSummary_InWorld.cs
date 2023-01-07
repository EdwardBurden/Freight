using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
	public abstract class BuildingSummary_InWorld : MonoBehaviour
	{
		[SerializeField] protected TMP_Text buildingNameText;
		protected Building building;


		public void Init(Building building)
		{
			this.building = building;
			if (building.storage != null)
			{
				building.storage.StorageUpdatedEvent += OnStorageUpdatedEvent;
			}
			Show();
		}

		private void OnStorageUpdatedEvent()
		{
			Show();
		}

		public virtual void Show()
		{
			buildingNameText.text = building.GetInteractableObjectName();

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
