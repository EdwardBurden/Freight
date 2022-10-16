using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class ProducerPanel : Panel
{
	[SerializeField]
	private TMP_Text nameField;

	private Building building;


	[SerializeField]
	private Transform wantsTransform;

	[SerializeField]
	private Transform acceptsTransform;

	protected void OnEnable()
	{
		BuildingController.BuildingSelectedEvent += BuildingController_BuildingSelectedEvent;
		BuildingController.BuildingDeSelectedEvent += BuildingController_BuildingDeSelectedEvent;
	}

	private void OnDisable()
	{
		BuildingController.BuildingSelectedEvent -= BuildingController_BuildingSelectedEvent;
		BuildingController.BuildingDeSelectedEvent -= BuildingController_BuildingDeSelectedEvent;
	}

	private void BuildingController_BuildingDeSelectedEvent(Building obj)
	{
		Hide();
	}

	private void BuildingController_BuildingSelectedEvent(Building obj)
	{
		Hide();
		if (obj is IProducer producer)
		{
			building = obj;
			nameField.text = building.GetObjectName();
			Show();
		}
	}


}
