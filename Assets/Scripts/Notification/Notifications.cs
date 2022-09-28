using System;
using System.Collections.Generic;

public class Notifications : Singleton<Notifications>
{
	public event Action<Notification> NotificationAddedEvent;
	public event Action NotificationEmptyEvent;

	private List<Notification> notifications = new List<Notification>();

	private void Awake()
	{
		_instance = this;
	}

	private void OnEnable()
	{
		ShipOrderQueue.OrderFailedEvent += ShipOrderFailed;
	}

	private void OnDisable()
	{
		ShipOrderQueue.OrderFailedEvent -= ShipOrderFailed;
	}

	private void ShipOrderFailed(ShipOrder obj, string message)
	{
		AddNotification(obj.shipObject.shipName, message);
	}

	private void AddNotification(string sender, string message)
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
