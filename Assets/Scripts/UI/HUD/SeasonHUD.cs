using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SeasonHUD : MonoBehaviour
{
	[SerializeField] private TMP_Text seasonText;

	private void OnEnable()
	{
		SeasonsController.SeasonStartedEvent += SeasonsController_SeasonStartedEvent;
		SeasonsController.SeasonEndedEvent += SeasonsController_SeasonEndedEvent;
	}

	private void SeasonsController_SeasonEndedEvent(SeasonType seasonType)
	{
		seasonText.text = "";
	}

	private void SeasonsController_SeasonStartedEvent(SeasonType seasonType)
	{
		seasonText.text = seasonType.ToString();
	}

	private void OnDisable()
	{
		SeasonsController.SeasonStartedEvent -= SeasonsController_SeasonStartedEvent;
		SeasonsController.SeasonEndedEvent -= SeasonsController_SeasonEndedEvent;
	}
}
