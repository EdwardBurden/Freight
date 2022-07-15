using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : Singleton<NotificationManager>
{
	public static event Action<Notification> NotificationAddedEvent;
	public static event Action NotificationEmptyEvent;

	private List<Notification> notifications = new List<Notification>();

	private void Awake()
	{
		_instance = this;
	}

	public void AddNotification(object sender, string message)
	{
		Notification notification = new Notification(sender.ToString(), message);
		notifications.Add(notification);
		NotificationAddedEvent?.Invoke(notification);
	}

	public void RemoveNotification(Notification notification) //called from ui
	{
		notifications.Remove(notification);
		if (notifications.Count == 0)
		{
			NotificationEmptyEvent?.Invoke();
		}
	}

}
