using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
	[SerializeField]
	protected GameObject root;

	private void OnEnable()
	{
		//UIManager.instance.RegisterPanel(this);
	}

	private void OnDisable()
	{
		//UIManager.instance.RemovePanel(this);
	}

	private void Start()
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
