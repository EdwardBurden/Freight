using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{//TODO
	public class ShipActionOptionsScreen : Screen
	{
		public static string PATH = "Prefabs/UI/InteractionActionOptionsDialog";
		public static event Action<ShipAction> ShipActionCreatedEvent;
		public static event Action<ShipAction> ShipActionCreationCancelledEvent;

		[SerializeField]
		private Transform options;

		private ShipObject shipObject;
		private InteractableObject obj;
		private Vector3 worldClickPosition;

		public static void Create(ShipObject shipObject, InteractableObject obj, ShipActionDefintion[] interactionActions, Vector3 worldClickPosition)
		{

			ShipActionOptionsScreen screen = Screen.Open(PATH) as ShipActionOptionsScreen;
			screen.Init(shipObject, obj, interactionActions, worldClickPosition);
			UIManager.instance.RegisterScreen(screen);
		}

		public void Init(ShipObject shipObject, InteractableObject obj, ShipActionDefintion[] interactionActions, Vector3 worldClickPosition)
		{
			this.shipObject = shipObject;
			this.obj = obj;
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
			//Hide();
			interactionOrder.OnOptionSelected(shipObject, obj, worldClickPosition, OnSelectionComplete);
		}

		private void OnSelectionComplete(ShipAction shipAction)
		{
			UIManager.instance.Close(this);
			ShipActionCreatedEvent?.Invoke(shipAction);
		}

		private void Open(Vector3 positionInWorld)
		{
			Vector3 hit = Camera.main.WorldToScreenPoint(positionInWorld);
			root.transform.GetComponent<RectTransform>().position = hit;
			//Show();
		}
	}
}