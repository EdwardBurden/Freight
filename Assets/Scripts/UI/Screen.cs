using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{ //TODO , remove this class, move clickback function into seperate component
	public abstract class Screen : MonoBehaviour
	{
		[SerializeField]
		private Button backbutton;

		[SerializeField]
		public GameObject root;

		private void Start()
		{
			backbutton?.onClick.RemoveAllListeners();
			backbutton?.onClick.AddListener(() => GameUIController.instance.BackOneScreen());
		}

		protected void SetRootPostion(Vector3 positionInWorld)
		{
			root.transform.GetComponent<RectTransform>().anchoredPosition = GameUIController.instance.GetScreenPosition(positionInWorld);
		}
	}
}