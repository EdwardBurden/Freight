using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{//TODO
	public class SelectActionForPanelScreen : Screen //plan to remove this and make ShipActionSelectScreen work for both 
	{
		public static string PATH = "Prefabs/UI/Screens/SelectActionForPanelScreen";

		[SerializeField]
		private Transform options;

		[SerializeField]
		private ShipActionIconButtonWidget iconButtonWidget;

		private Action<ShipActionDefintion> callBack;

		public static void Create(ShipActionDefintion[] interactionActions, Action<ShipActionDefintion> callBack)
		{
			SelectActionForPanelScreen screen = Screen.Open(PATH) as SelectActionForPanelScreen;
			screen.Init(interactionActions, callBack);
			UIManager.instance.RegisterScreen(screen);
		}

		public void Init(ShipActionDefintion[] interactionActions, Action<ShipActionDefintion> callBack)
		{
			this.callBack = callBack;
			Clear(options);
			Show();

			foreach (ShipActionDefintion action in interactionActions.OrderBy(x => x.uiOrder))
			{
				ShipActionIconButtonWidget shipActionIconWidget = Instantiate(iconButtonWidget, options);
				shipActionIconWidget.ShowAction(action, () => OptionSelected(action));
			}
		}

		private void OptionSelected(ShipActionDefintion interactionOrder)
		{
			callBack?.Invoke(interactionOrder);
		}
	}
}