using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{

	public class FarmSummary_InWorld : BuildingSummary_InWorld
	{
		[SerializeField] private ResourceProducedWidget buttonWidget;
		[SerializeField] private Transform resourcesTransform;

		private ProducerLevelBuilding farm => building as ProducerLevelBuilding;

		public override void Show()
		{
			base.Show();

			GameObjectUtils.Clear(resourcesTransform);
			ResourceDefintion resourceDefintion = farm.GetResourceProduced();
			ResourceProducedWidget resourceIconButton = Instantiate(buttonWidget, resourcesTransform);
			int stored = building.storage.GetStorage(resourceDefintion);
			resourceIconButton.ShowResource(resourceDefintion , stored);
		}

	}
}