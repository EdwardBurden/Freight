using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
	public class NotificationPanel : Panel
	{
		[SerializeField]
		private GameObject notificationWidget;

		[SerializeField]
		private Transform list;

		protected  void Start()
		{
			NotificationManager.instance.NotificationAddedEvent += NotificationManager_NotificationAddedEvent;
			NotificationManager.instance.NotificationEmptyEvent += NotificationManager_NotificationEmptyEvent;
		}
		private void OnDestroy()
		{
			NotificationManager.instance.NotificationAddedEvent += NotificationManager_NotificationAddedEvent;
			NotificationManager.instance.NotificationEmptyEvent += NotificationManager_NotificationEmptyEvent;
		}

		private void NotificationManager_NotificationEmptyEvent()
		{
			Hide();
		}



		private void NotificationManager_NotificationAddedEvent(Notification obj)
		{
			Show();
			GameObject gameObject = Instantiate(notificationWidget, list);
			gameObject.GetComponentInChildren<TMP_Text>().text = obj.FormatNotification();
			gameObject.GetComponentInChildren<Button>().onClick.AddListener(() => RemoveNotification(obj, gameObject));
		}

		private void RemoveNotification(Notification notification, GameObject gameObject)
		{
			Destroy(gameObject);
			NotificationManager.instance.RemoveNotification(notification);
		}
	}
}