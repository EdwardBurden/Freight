
using System;
using UnityEngine;

namespace UI
{
	public class ResourceSelectScreen : Screen
	{
		public static string PATH = "Prefabs/UI/Screens/ResourceSelectScreen";

		[SerializeField]
		private Transform options;

		private ResourceDefintion[] resources;
		private Action<ResourceDefintion> callBack;

		public static void Create(ResourceDefintion[] resources, Vector3 clickPosition, Action<ResourceDefintion> callBack)
		{
			ResourceSelectScreen screen = Screen.Open(PATH) as ResourceSelectScreen;
			screen.Init(resources, clickPosition, callBack);
			UIManager.instance.RegisterScreen(screen, false);
		}

		public void Init(ResourceDefintion[] resources, Vector3 clickPosition, Action<ResourceDefintion> callBack)
		{
			this.resources = resources;
			this.callBack = callBack;
			Clear(options);
			Open(clickPosition);
			foreach (ResourceDefintion resource in resources)
			{
				resource.CreateResourceSelectionButton(options, () => OptionSelected(resource));
			}
		}

		private void OptionSelected(ResourceDefintion resource)
		{
			callBack?.Invoke(resource);
		}
	}
}