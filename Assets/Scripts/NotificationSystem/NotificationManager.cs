using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo, remove singleton, use event from shipaction list monobehaviour to get error output.
public class NotificationManager : Singleton<NotificationManager>
{
	public event Action<Notification> NotificationAddedEvent;
	public event Action NotificationEmptyEvent;

	private List<Notification> notifications = new List<Notification>();

	private void Awake()
	{
		_instance = this;
	}

	public void AddNotification(string sender, string message)
	{
		Notification notification = new Notification(sender, message);
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
