using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
	public class InWorld : MonoBehaviour
	{
		private void Start()
		{
			this.GetComponent<Canvas>().worldCamera = UIManager.instance.uiCamera;
		}
	}
}