using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
	public abstract class Screen : MonoBehaviour
	{

		[SerializeField]
		public GameObject root;
		public static Screen Open(string path)
		{
			GameObject obj = Instantiate(Resources.Load<GameObject>(path), GameUIManager.instance.screens.transform);
			return obj.GetComponent<Screen>();
		}

		public void CloseThis()
		{
			GameUIManager.instance.Close(this);
		}

		public virtual void Destroy()
		{
			Destroy(this.gameObject);
		}

		public virtual void Hide()
		{
			root.SetActive(false);
		}

		public virtual void Show()
		{
			root.SetActive(true);
		}

		protected void Clear(Transform transform)
		{
			foreach (Transform item in transform)
			{
				Destroy(item.gameObject);
			}
		}

		protected void Open(Vector3 positionInWorld)
		{
			root.transform.GetComponent<RectTransform>().anchoredPosition = UIUtils.GetAnchoredPostionFromWorldSpacePoint(positionInWorld, GameUIManager.instance.screens);
			//Vector3 hit = GameUIManager.instance.uiCamera.WorldToScreenPoint(positionInWorld);
			//root.transform.GetComponent<RectTransform>().position = hit;
		}
	}
}