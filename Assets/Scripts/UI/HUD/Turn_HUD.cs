using TMPro;
using UI;
using UnityEngine;
namespace UI
{
	public class Turn_HUD : HUD
	{
		[SerializeField] private TMP_Text turnText;

		private void OnEnable()
		{
			TurnsController.TurnStartedEvent += OnTurnStart;
			TurnsController.TurnEndedEvent += OnTurnEnd;
		}

		private void OnDisable()
		{
			TurnsController.TurnStartedEvent -= OnTurnStart;
			TurnsController.TurnEndedEvent -= OnTurnEnd;
		}

		private void Start()
		{
			turnText.text = 0.ToString();
		}

		private void OnTurnEnd(int turn)
		{
			turnText.text = "";
		}

		private void OnTurnStart(int turn)
		{
			turnText.text = turn.ToString();
		}
	}
}