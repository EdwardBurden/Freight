using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct Notification
{
	public string sender;
	public string message;

	public Notification(string sender, string message)
	{
		this.sender = sender;
		this.message = message;
	}

	public string FormatNotification()
	{
		return $"{sender} says : {message}";

	}
}
