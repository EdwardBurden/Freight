using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
namespace UI
{
	public class InteractableHoverInScene : InScene //shoudl be in game now
	{
		[SerializeField]
		private TMP_Text nameText;

		public void OnHover(InteractableObject obj)
		{
			nameText.text = obj.GetObjectName();
		}
	}
}