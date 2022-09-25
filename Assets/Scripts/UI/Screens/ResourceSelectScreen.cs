
using System;
using UnityEngine;

namespace UI
{
	public class ResourceSelectScreen : Screen
	{
		public static string PATH = "UI/Screens/ResourceSelectScreen";

		[SerializeField]
		private Transform options;

		private ResourceDefintion[] resources;
		private Action<ResourceDefintion> callBack;

		public static void Create(ResourceDefintion[] resources, Vector3 clickPosition, Action<ResourceDefintion> callBack, bool closeOtherMenus)
		{
			ResourceSelectScreen screen = UIManager.instance.OpenScreen(PATH, closeOtherMenus) as ResourceSelectScreen;
			screen.Init(resources, clickPosition, callBack);
		}

		public void Init(ResourceDefintion[] resources, Vector3 clickPosition, Action<ResourceDefintion> callBack)
		{
			this.resources = resources;
			this.callBack = callBack;

			GameObjectUtils.Clear(options);
			SetRootPostion(clickPosition);
			ShowResoures();
		}

		private void ShowResoures()
		{
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