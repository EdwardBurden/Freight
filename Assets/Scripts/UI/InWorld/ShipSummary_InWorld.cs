using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace UI
{
    public abstract class ShipSummary_InWorld : MonoBehaviour
	{
		[SerializeField] protected TMP_Text shipNameText;
		protected Ship ship;

		public virtual void Init(Ship ship)
		{
			this.ship = ship;
			Show();
		}

		public virtual void Show()
		{
			shipNameText.text = ship.GetInteractableObjectName();
		}

		public void OnShipHover()
		{
			Show();
			//throw new NotImplementedException();
		}

		public void OffShipHover()
		{
			Show();
			//throw new NotImplementedException();
		}
	}
}