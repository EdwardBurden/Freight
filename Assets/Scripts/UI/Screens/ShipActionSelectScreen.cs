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

		private Vector3 position;


		private Action<ShipActionDefintion> callBack;

		public static void Create(Vector3 position, ShipActionDefintion[] interactionActions, Action<ShipActionDefintion> callBack, bool inScreenSpace = false)
		{
			ShipActionSelectScreen screen = Screen.Open(PATH) as ShipActionSelectScreen;
			screen.Init(position, inScreenSpace, interactionActions, callBack);
			UIManager.instance.RegisterScreen(screen);
		}

		public void Init(Vector3 position, bool inScreenSpace, ShipActionDefintion[] interactionActions, Action<ShipActionDefintion> callBack)
		{
			this.position = position;
			this.callBack = callBack;
			Clear(options);
			if (!inScreenSpace)
			{
				Open(position);
			}
			else
			{
				root.transform.GetComponent<RectTransform>().position = position;
			}

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