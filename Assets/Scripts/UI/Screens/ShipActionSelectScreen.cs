using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{//TODO
	public class ShipActionSelectScreen : Screen
	{
		public static string PATH = "Prefabs/UI/Screens/ShipActionSelectScreen";

		[SerializeField]
		private Transform options;

		[SerializeField]
		private ShipActionIconButtonWidget iconButtonWidget;

		private RightClickContextActionInfo shipActionSelectionInfo;
		private Action<ShipActionDefintion, RightClickContextActionInfo> callBack;

		public static void Create(RightClickContextActionInfo shipActionSelectionInfo, ShipActionDefintion[] interactionActions, Action<ShipActionDefintion, RightClickContextActionInfo> callBack)
		{
			ShipActionSelectScreen screen = Screen.Open(PATH) as ShipActionSelectScreen;
			screen.Init(shipActionSelectionInfo, interactionActions, callBack);
			UIManager.instance.RegisterScreen(screen);
		}

		public void Init(RightClickContextActionInfo shipActionSelectionInfo, ShipActionDefintion[] interactionActions, Action<ShipActionDefintion, RightClickContextActionInfo> callBack)
		{
			this.shipActionSelectionInfo = shipActionSelectionInfo;
			this.callBack = callBack;
			Clear(options);
			Open(shipActionSelectionInfo.clickPosition);

			foreach (ShipActionDefintion action in interactionActions.OrderBy(x => x.uiOrder))
			{
				ShipActionIconButtonWidget shipActionIconWidget = Instantiate(iconButtonWidget, options);
				shipActionIconWidget.ShowAction(action, () => OptionSelected(action));
			}
		}

		private void OptionSelected(ShipActionDefintion interactionOrder)
		{
			callBack?.Invoke(interactionOrder, shipActionSelectionInfo);
		}
	}
}