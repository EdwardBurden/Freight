using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
	public class Panel : MonoBehaviour
	{
		[SerializeField]
		protected GameObject root;

		protected virtual void Start()
		{
			Hide();
		}

		public void Show()
		{
			root.SetActive(true);
		}

		public void Hide()
		{
			root.SetActive(false);
		}
	}
}