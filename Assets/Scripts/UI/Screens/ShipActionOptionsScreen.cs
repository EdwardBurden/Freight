using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{//TODO
	public class ShipActionOptionsScreen : Screen
	{
		public static string PATH = "Prefabs/UI/InteractionActionOptionsScreen";

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
			Clear(options);
			Open(shipActionSelectionInfo.clickPosition);
			foreach (ShipActionDefintion action in interactionActions)
			{
				action.CreateActionSelectionButton(options, () => OptionSelected(action));
			}

		}

		private void OptionSelected(ShipActionDefintion interactionOrder)
		{
			callBack?.Invoke(interactionOrder, shipActionSelectionInfo);
		}
	}
}