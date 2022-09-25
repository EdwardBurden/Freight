using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public abstract class Screen : MonoBehaviour
	{
		[SerializeField]
		private Button backbutton;

		[SerializeField]
		public GameObject root;

		private void Start()
		{
			backbutton?.onClick.RemoveAllListeners();
			backbutton?.onClick.AddListener(() => UIManager.instance.BackOneScreen());
		}

		protected void SetRootPostion(Vector3 positionInWorld)
		{
			root.transform.GetComponent<RectTransform>().anchoredPosition = UIUtils.GetAnchoredPostionFromWorldSpacePoint(positionInWorld, UIManager.instance.screens);
		}
	}
}