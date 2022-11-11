using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
	//TODO
	public class StorageChanged_InCamera : InCamera
	{
		private void Start()
		{
			StartCoroutine(Die());
		}

		private IEnumerator Die()
		{
			yield return new WaitForSeconds(1);
			Destroy(this.gameObject);
		}
	}
}
