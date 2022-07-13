using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Screen : MonoBehaviour
{
	[SerializeField]
	public GameObject root;
	public static Screen Open(string path)
	{
		GameObject obj = Instantiate(Resources.Load<GameObject>(path), UIManager.instance.screentransform); ;
		return obj.GetComponent<Screen>();
	}

	public virtual void Close()
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
}
