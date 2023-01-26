using TMPro;
using UI;
using UnityEngine;
namespace UI
{
	public class Season_HUD : HUD
	{
		[SerializeField] private TMP_Text seasonText;

		private void OnEnable()
		{
			//SeasonsController.SeasonStartedEvent += OnSeasonStarted;
			//SeasonsController.SeasonEndedEvent += OnSeasonEnded;
		}

		private void OnDisable()
		{
			//SeasonsController.SeasonStartedEvent -= OnSeasonStarted;
			//SeasonsController.SeasonEndedEvent -= OnSeasonEnded;
		}

		private void OnSeasonEnded(SeasonType seasonType)
		{
			seasonText.text = "";
		}

		private void OnSeasonStarted(SeasonType seasonType)
		{
			seasonText.text = seasonType.ToString();
		}
	}
}