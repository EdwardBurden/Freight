using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class NotificationPanel : Panel
{
	[SerializeField]
	private GameObject notificationWidget;

	[SerializeField]
	private Transform list;

	public void Awake()
	{
		NotificationManager.NotificationAddedEvent += NotificationManager_NotificationAddedEvent;
		NotificationManager.NotificationEmptyEvent += NotificationManager_NotificationEmptyEvent;
	}
	private void OnDestroy()
	{
		NotificationManager.NotificationAddedEvent += NotificationManager_NotificationAddedEvent;
		NotificationManager.NotificationEmptyEvent += NotificationManager_NotificationEmptyEvent;
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
