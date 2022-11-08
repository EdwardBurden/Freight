using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
	//TODO , remove this class
	public class Panel : MonoBehaviour
	{
		[SerializeField]
		protected GameObject root;

		protected  void Awake()
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