using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class InteractionActionOptionsDialog : MonoBehaviour
	{
		public static event Action<ShipAction> ShipActionCreatedEvent;

		[SerializeField]
		private GameObject root;

		[SerializeField]
		private Transform options;


		private ShipObject shipObject;
		private InteractableObject obj;
		private ShipActionDefintion[] interactionActions;
		private Vector3 worldClickPosition;

		private void Awake()
		{
			ShipManager.ShipOptionsOpened += DisplayOptions; //TODO replace with UIMAnager
		}

		private void OnDestroy()
		{
			ShipManager.ShipOptionsOpened -= DisplayOptions;// TODO replace with UIMAnager
		}


		private void Start()
		{
			Hide();
		}

		public void DisplayOptions(ShipObject shipObject, InteractableObject obj, ShipActionDefintion[] interactionActions, Vector3 worldClickPosition)
		{
			this.shipObject = shipObject;
			this.obj = obj;
			this.interactionActions = interactionActions;
			this.worldClickPosition = worldClickPosition;


			Clear();
			Open(worldClickPosition);
			foreach (ShipActionDefintion action in interactionActions)
			{
				GameObject gameObject = action.CreateActionSelectionButton(options);
				Button button = gameObject.AddComponent<Button>();
				button.onClick.AddListener(() => OptionSelected(action));
			}

		}

		private void Clear()
		{
			foreach (Transform item in options)
			{
				Destroy(item.gameObject);
			}
		}

		private void OptionSelected(ShipActionDefintion interactionOrder)
		{
			Hide();
			interactionOrder.OnOptionSelected(shipObject, obj, worldClickPosition, OnSelectionComplete);
		}

		private void OnSelectionComplete(ShipAction shipAction)
		{
			Hide();
			ShipActionCreatedEvent?.Invoke(shipAction);
		}

		private void Open(Vector3 positionInWorld)
		{
			//Vector3 hit = Camera.main.WorldToScreenPoint(positionInWorld);
			//root.transform.position = hit;
			Show();
		}
		public void Show()
		{
			root.SetActive(true);
		}

		public void Hide()
		{
			root.SetActive(false);
		}
	}
}