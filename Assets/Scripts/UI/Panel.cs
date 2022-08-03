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

		public virtual void Show()
		{
			root.SetActive(true);
		}

		public virtual void Hide()
		{
			root.SetActive(false);
		}
	}
}