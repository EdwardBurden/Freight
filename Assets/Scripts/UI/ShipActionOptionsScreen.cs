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

		[SerializeField]
		private Transform options;

		private ShipActionSelectionInfo shipActionSelectionInfo;
		private Action<ShipActionDefintion, ShipActionSelectionInfo> callBack;

		public static void Create(ShipActionSelectionInfo shipActionSelectionInfo, ShipActionDefintion[] interactionActions, Action<ShipActionDefintion, ShipActionSelectionInfo> callBack)
		{
			ShipActionOptionsScreen screen = Screen.Open(PATH) as ShipActionOptionsScreen;
			screen.Init(shipActionSelectionInfo, interactionActions, callBack);
			UIManager.instance.RegisterScreen(screen);
		}

		public void Init(ShipActionSelectionInfo shipActionSelectionInfo, ShipActionDefintion[] interactionActions, Action<ShipActionDefintion, ShipActionSelectionInfo> callBack)
		{
			this.shipActionSelectionInfo = shipActionSelectionInfo;
			this.callBack = callBack;
			Clear();
			Open(shipActionSelectionInfo.clickPosition);
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
			callBack?.Invoke(interactionOrder, shipActionSelectionInfo);
		}

		private void Open(Vector3 positionInWorld)
		{
			Vector3 hit = Camera.main.WorldToScreenPoint(positionInWorld);
			root.transform.GetComponent<RectTransform>().position = hit;
			//Show();
		}
	}
}