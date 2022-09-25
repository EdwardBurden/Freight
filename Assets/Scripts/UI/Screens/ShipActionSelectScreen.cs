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
		public static string PATH = "UI/Screens/ShipActionSelectScreen";

		[SerializeField]
		private Transform options;

		[SerializeField]
		private ShipActionIconButtonWidget iconButtonWidget;

		private Vector3 position;


		private Action<ShipActionDefintion> callBack;

		public static void Create(Vector3 position, ShipActionDefintion[] interactionActions, Action<ShipActionDefintion> callBack, bool inScreenSpace = false)
		{
			ShipActionSelectScreen screen = UIManager.instance.OpenScreen(PATH) as ShipActionSelectScreen;
			screen.Init(position, inScreenSpace, interactionActions, callBack);
		}

		public void Init(Vector3 position, bool inScreenSpace, ShipActionDefintion[] interactionActions, Action<ShipActionDefintion> callBack)
		{
			this.position = position;
			this.callBack = callBack;

			GameObjectUtils.Clear(options);
			SetPosition(inScreenSpace);
			ShowActions(interactionActions);
		}

		private void SetPosition(bool inScreenSpace)
		{
			if (!inScreenSpace)
			{
				SetRootPostion(position);
			}
			else
			{
				root.transform.GetComponent<RectTransform>().position = position;
			}
		}

		private void ShowActions(ShipActionDefintion[] shipActionDefintions)
		{
			foreach (ShipActionDefintion action in shipActionDefintions.OrderBy(x => x.uiOrder))
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