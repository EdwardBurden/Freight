using System;
using System.Collections.Generic;

public class NotificationsController : Singleton<NotificationsController>, IGameController
{
	public static event Action<Notification> NotificationAddedEvent;
	public static event Action NotificationEmptyEvent;

	private List<Notification> notifications = new List<Notification>();

	private void Awake()
	{
		_instance = this;
	}

	public void Init()
	{
		notifications.Clear();
	}

	public void AddNotification(string sender, string message)
	{
		Notification notification = new Notification(sender, message);
		notifications.Add(notification);
		NotificationAddedEvent?.Invoke(notification);
	}

	public void RemoveNotification(Notification notification)
	{
		notifications.Remove(notification);
		if (notifications.Count == 0)
		{
			NotificationEmptyEvent?.Invoke();
		}
	}
}
