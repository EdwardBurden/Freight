
using System;
using UnityEngine;

namespace UI
{
	public class ResourceSelectScreen : Screen
	{
		public static string PATH = "Prefabs/UI/ResourceSelectScreen";

		[SerializeField]
		private Transform options;

		private ResourceData[] resources;
		private Action<ResourceData> callBack;

		public static void Create(ResourceData[] resources, Vector3 clickPosition, Action<ResourceData> callBack)
		{
			ResourceSelectScreen screen = Screen.Open(PATH) as ResourceSelectScreen;
			screen.Init(resources, clickPosition, callBack);
			UIManager.instance.RegisterScreen(screen, false);
		}

		public void Init(ResourceData[] resources, Vector3 clickPosition, Action<ResourceData> callBack)
		{
			this.resources = resources;
			this.callBack = callBack;
			Clear(options);
			Open(clickPosition);
			foreach (ResourceData resource in resources)
			{
				resource.CreateResourceSelectionButton(options, () => OptionSelected(resource));
			}
		}

		private void OptionSelected(ResourceData resource)
		{
			callBack?.Invoke(resource);
		}
	}
}